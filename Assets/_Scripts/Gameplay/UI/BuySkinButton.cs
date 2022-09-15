using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BuySkinButton : BuyButton
{
    public List<SkinToBuy> SkinsToBuy = new List<SkinToBuy>();



    public override void BuyElement(ItemToBuy item)
    {
        var skinItem = (SkinToBuy)item;

        var data = ServiceLocator.GetService<GameProgressionService>().GameData;

        if (data.Tokens - price < 0) return;

        GameDataEventManager.NotifyOnSkinAdded(skinItem.IconId);
       
        GameDataEventManager.NotifyOnCoinsChange(data.Tokens - price);

        GameDataEventManager.NotifyOnBuyMade(Id);
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
