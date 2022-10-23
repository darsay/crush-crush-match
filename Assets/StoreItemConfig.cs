using System.Collections.Generic;

[System.Serializable]
public class StoreItemConfig
{
    public int id;
    public int RewardAmount;
    public int Price;
    public List<string> Items = new List<string>();
    public bool IsIAP;
    public List<string> ContentType = new List<string>();
}
