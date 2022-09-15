using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuBottomBar : MonoBehaviour
{
    [SerializeField] RectTransform[] buttons;
    [SerializeField] float defaultSize;
    [SerializeField] float selectedSize;

    int _currentMenu = 1;

    private void Start()
    {
        SetCurrentMenu(_currentMenu);
    }

    public void SetCurrentMenu(int i)
    {
        buttons[_currentMenu].DOSizeDelta(Vector2.one * defaultSize, 0.3f);
        _currentMenu = i;
        buttons[_currentMenu].DOSizeDelta(Vector2.one * selectedSize, 0.3f);
    }
}
