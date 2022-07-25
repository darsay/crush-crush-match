using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinScreen : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        BoardEventManager.OnWin += OnWin;
    }

    void OnWin()
    {
        canvasGroup.DOFade(1, 0.5f);
    }
}
