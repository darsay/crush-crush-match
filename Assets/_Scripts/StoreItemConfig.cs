using System.Collections.Generic;

[System.Serializable]
public class StoreItemConfig
{
    public int id;
    public string Name;
    public int RewardAmount;
    public int Price;
    public List<string> Items = new List<string>();
    public bool IsIAP;
    public string ContentType;
}
