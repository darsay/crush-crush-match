using System.Collections.Generic;

public class GameConfigService : IService
{
    public int InitialTokens = 0;
    public int TokensPerWin = 0;
    public int TokensToRetry = 0;

    public int InitialColumnsEraser = 0;
    public int InitialRowEraser = 0;
    public int InitialCellEraser = 0;
    public int InitialShuffle = 0;

    public int InitialPlayerIcon = 0;
    public int InitialCrushIcon = 0;

    public List<int> InitialSkins;

    public List<StoreItemConfig> StoreConfig;

    public void Initialize(RemoteConfigGameService dataProvider)
    {
        InitialTokens = dataProvider.GetInt("PlayerInitialTokens", 100);
        TokensPerWin = dataProvider.GetInt("TokensPerWin", 150);
        TokensToRetry = dataProvider.GetInt("TokensToRetry", 150);

        InitialColumnsEraser = dataProvider.GetInt("PlayerInitialColumnEraser", 5);
        InitialRowEraser = dataProvider.GetInt("PlayerInitialRowEraser", 5);
        InitialCellEraser = dataProvider.GetInt("PlayerInitialCellEraser", 5);
        InitialShuffle = dataProvider.GetInt("PlayerInitialShuffle", 5);

        InitialPlayerIcon = dataProvider.GetInt("DefaultPlayerIcon", 0);
        InitialCrushIcon = dataProvider.GetInt("DefaultCrushIcon", 1);

        var iconList = dataProvider.Get<GameIconsList>("PlayerIcons");
        InitialSkins = dataProvider.Get<GameIconsList>("PlayerIcons")?.DefaultIcons;
        StoreConfig = dataProvider.Get<StoreConfig>("StoreItems")?.Items;

        if (InitialSkins == null)
        {
            InitialSkins = new List<int>() { 0, 1 };
        }

    }

    public void Clear()
    {

    }
}