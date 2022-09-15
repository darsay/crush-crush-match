using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tip", menuName = "Tips/RegularTip", order = 1)]
public class LoadingSceneTip : ScriptableObject
{
    public string Description;

    public Sprite Sprite;
}
