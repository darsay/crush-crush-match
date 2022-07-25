using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoseScreen : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        BoardEventManager.OnDefeat += OnDefeat;
    }

    void OnDefeat()
    {
        canvasGroup.DOFade(1, 0.5f);
    }
}
