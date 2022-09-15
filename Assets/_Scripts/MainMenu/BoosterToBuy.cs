using UnityEngine;

[System.Serializable]
public class BoosterToBuy : ItemToBuy
{
    public ActiveBooster boosterType;
    public int amount;
}

[System.Serializable]
public class SkinToBuy : ItemToBuy
{
    public int IconId;
}

[System.Serializable]
public class ItemToBuy
{

}
