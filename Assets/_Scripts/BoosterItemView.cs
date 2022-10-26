using System.Collections.Generic;

public class BoosterItemView : StoreItemView
{
    public void BuyElement(string item, GameData data)
    {
        

        switch (item)
        {
            case "CellEraser":
                GameDataEventManager.NotifyOnCellEraserChange(data.CellEraser + _config.RewardAmount);
                break;
            case "ColumnEraser":
                GameDataEventManager.NotifyOnColumnEraserChange(data.ColumnEraser + _config.RewardAmount);
                break;
            case "LineEraser":
                GameDataEventManager.NotifyOnRowEraserChange(data.RowEraser + _config.RewardAmount);
                break;
            case "Shuffle":
                GameDataEventManager.NotifyOnShuffleChange(data.ShuffleBooster + _config.RewardAmount);
                break;
        }
    }

    public override void Buy()
    {
        var data = ServiceLocator.GetService<GameProgressionService>().GameData;
        if (data.Tokens - _config.Price < 0) return;

        foreach (var element in _config.Items)
        {
            BuyElement(element, data);
        }

        ServiceLocator.GetService<GameProgressionService>().SaveGameData();

        GameDataEventManager.NotifyOnCoinsChange(data.Tokens - _config.Price);
    }

    protected override void UpdateVisuals()
    {
        base.UpdateVisuals();
        priceText.text = _config.Price.ToString();
    }
}
