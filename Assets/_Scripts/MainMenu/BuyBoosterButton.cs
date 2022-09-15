using System.Collections;
using System.Collections.Generic;

public class BuyBoosterButton : BuyButton
{
    public List<BoosterToBuy> boosters = new List<BoosterToBuy>(); 

    public override void BuyElement(ItemToBuy element)
    {
        var bosterElement = (BoosterToBuy)element;

        var data = ServiceLocator.GetService<GameProgressionService>().GameData;

        if (data.Tokens - price < 0) return;

        switch (bosterElement.boosterType)
        {
            case ActiveBooster.CellBreaker:
                GameDataEventManager.NotifyOnCellEraserChange(data.CellEraser + bosterElement.amount);
                break;
            case ActiveBooster.ColumnBreaker:
                GameDataEventManager.NotifyOnColumnEraserChange(data.ColumnEraser + bosterElement.amount);
                break;
            case ActiveBooster.LineBreaker:
                GameDataEventManager.NotifyOnRowEraserChange(data.RowEraser + bosterElement.amount);
                break;
            case ActiveBooster.Shuffle:
                GameDataEventManager.NotifyOnShuffleChange(data.ShuffleBooster + bosterElement.amount);
                break;
        }



        GameDataEventManager.NotifyOnCoinsChange(data.Tokens - price);
    }

    public override void BuyPack()
    {
        foreach (var element in boosters)
        {
            BuyElement(element);
        }

        ServiceLocator.GetService<GameProgressionService>().SaveGameData();
    }
}
