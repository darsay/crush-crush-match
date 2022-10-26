using System;
using System.Collections;
using UnityEngine;

public class SkinsItemView : StoreItemView
{
    IIAPGameService _iapService;

    protected override void Start()
    {
        base.Start();
        _iapService = ServiceLocator.GetService<IIAPGameService>();

        if (!_buttonStatus)
        {
            priceText.text = "Sold";
            return;
        }

        if (!_config.IsIAP) return;


        if (!_iapService.IsReady())
        {
            _button.interactable = false;

            StartCoroutine(WaitForIAPReady());
            priceText.text = "Loading";

        }
        else
        {
            priceText.text = _iapService.GetLocalizedPrice(_config.Name);
        }
    }

    IEnumerator WaitForIAPReady()
    {
        while (!_iapService.IsReady())
        {
            yield return new WaitForSeconds(0.5f);
        }

        priceText.text = _iapService.GetLocalizedPrice(_config.Name);
        _button.interactable = true;
    }

    public void BuyElement(string item)
    {
        var data = ServiceLocator.GetService<GameProgressionService>().GameData;

        int iconId = Int32.Parse(item.Substring(item.Length - 2));

        GameDataEventManager.NotifyOnSkinAdded(iconId);
    }

    public override async void Buy()
    {
        var data = ServiceLocator.GetService<GameProgressionService>().GameData;
        if (data.Tokens - _config.Price < 0) return;

        if (!_config.IsIAP)
        {
            BuyAllElements();
        }
        else
        {
            if (await _iapService.StartPurchase(_config.Name))
            {
                BuyAllElements();
                priceText.text = "Sold";
            }
            else
            {
                Debug.LogError("Purchase failed");
            }
        }

        GameDataEventManager.NotifyOnBuyMade(_config.id);

        ServiceLocator.GetService<GameProgressionService>().SaveGameData();
    }

    void BuyAllElements()
    {
        foreach (var element in _config.Items)
        {
            BuyElement(element);
        }

        _button.enabled = false;


    }
}
