using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActiveBoosterBackground : MonoBehaviour
{

    SpriteRenderer activeBoosterBg;
    [SerializeField] float animationTime;
    [SerializeField] float maxOpacity;


    private void Awake()
    {
        activeBoosterBg = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        BoardEventManager.OnActiveBoosterSet += DoActiveBoosterBackgroundAnim;
    }

    private void OnDisable()
    {
        BoardEventManager.OnActiveBoosterSet -= DoActiveBoosterBackgroundAnim;
    }


    void DoActiveBoosterBackgroundAnim(ActiveBooster booster)
    {
        if(booster == ActiveBooster.None)
        {
            activeBoosterBg.DOFade(0, animationTime);
        }
        else
        {
            activeBoosterBg.DOFade(maxOpacity, animationTime);
        }
    }
}
