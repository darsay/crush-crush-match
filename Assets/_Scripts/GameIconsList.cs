using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameIconsList : ScriptableObject
{
    public List<int> IconsList = new List<int>();

    public void AddIcon(int newIcon)
    {
        if (IconsList.Contains(newIcon)) return;

        IconsList.Add(newIcon);
    }
}
