using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BuySkinButton : BuyButton
{
    public List<SkinToBuy> SkinsToBuy = new List<SkinToBuy>();

    [SerializeField]
    string productName;

    private void Start()
    {
        var _buttonStatus = ServiceLocator.GetService<GameProgressionService>().GameData.ShopItems.Contains(Id);

        if (!_buttonStatus)
        {
            _button.enabled = false;
            priceTag.text = "Sold";
            return;
        }

        if (!isIAP) return;


        if (!_iapService.IsReady())
        {
            _button.interactable = false;

            StartCoroutine(WaitForIAPReady());
            priceTag.text = "Loading";

        }
        else
        {
            priceTag.text = _iapService.GetLocalizedPrice(productName);
        }

    }

    IEnumerator WaitForIAPReady()
    {
        while (!_iapService.IsReady())
        {
            yield return new WaitForSeconds(0.5f);
        }

        priceTag.text = _iapService.GetLocalizedPrice(productName);
        _button.interactable = true;
    }

    public override async void BuyElement(ItemToBuy item)
    {
        var skinItem = (SkinToBuy)item;

        var data = ServiceLocator.GetService<GameProgressionService>().GameData;

        if (!isIAP)
        {
            if (data.Tokens - price < 0) return;
            GameDataEventManager.NotifyOnCoinsChange(data.Tokens - price);

            GameDataEventManager.NotifyOnSkinAdded(skinItem.IconId);


            GameDataEventManager.NotifyOnBuyMade(Id);
        }
        else
        {
            if (await _iapService.StartPurchase(productName))
            {
                GameDataEventManager.NotifyOnSkinAdded(skinItem.IconId);


                GameDataEventManager.NotifyOnBuyMade(Id);

                _button.enabled = false;
            }
            else
            {
                Debug.LogError("Purchase failed");
            }
        }


    }

    public override void BuyPack()
    {
        foreach (var element in SkinsToBuy)
        {
            BuyElement(element);
        }

        ServiceLocator.GetService<GameProgressionService>().SaveGameData();
    }
}
