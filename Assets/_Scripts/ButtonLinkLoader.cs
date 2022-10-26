using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLinkLoader : MonoBehaviour
{
    [SerializeField]
    string linkToLoad;

    public void LoadLink()
    {
        Application.OpenURL(linkToLoad);
    }
}
