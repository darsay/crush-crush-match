using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ActiveBoosterTooltip : MonoBehaviour
{
    [SerializeField] Image toolTipIcon;
    [SerializeField] TextMeshProUGUI textDescription;
    RectTransform _rt;

    [SerializeField] float animTime;
    [SerializeField] float hidePosition;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        BoardEventManager.OnActiveBoosterSet += OnBoosterSet;
    }

    void OnBoosterSet(ActiveBooster booster)
    {
        if (booster == ActiveBooster.None)
        {
            Hide();
        }
    }

    public void Show(string description, Sprite img)
    {
        textDescription.text = description;
        toolTipIcon.sprite = img;

        _rt.DOAnchorPosY(0, animTime);
    }

    void Hide()
    {
        _rt.DOAnchorPosY(hidePosition, animTime);
    }
}
