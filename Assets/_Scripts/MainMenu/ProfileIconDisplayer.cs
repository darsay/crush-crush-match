using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProfileIconDisplayer : MonoBehaviour
{
    [SerializeField] bool isPlayer;

    Action<Sprite> iconDisplayer;

    private void Awake()
    {
        iconDisplayer = DisplayIcon;
    }

    private void OnEnable()
    {
        if (isPlayer)
        {
            GameDataEventManager.OnPlayerIconChange += SetIcon;
        }
        else
        {
            GameDataEventManager.OnCrushIconChange += SetIcon;
        }
    }

    private void OnDisable()
    {
        if (isPlayer)
        {
            GameDataEventManager.OnPlayerIconChange -= SetIcon;
        }
        else
        {
            GameDataEventManager.OnCrushIconChange -= SetIcon;
        }
    }

    private void Start()
    {

       int icon = isPlayer ? ServiceLocator.GetService<GameProgressionService>().GameData.PlayerIcon :
          ServiceLocator.GetService<GameProgressionService>().GameData.CrushIcon;
      
       SetIcon(icon);
    }

    void SetIcon(int icon)
    {
        ServiceLocator.GetService<IconLoaderService>().LoadIcon(icon, DisplayIcon);
    }

    void DisplayIcon(Sprite icon)
    {
        Image image;
        SpriteRenderer sRenderer;

        if (TryGetComponent<Image>(out image))
        {
            image.sprite = icon;
        }
        else if (TryGetComponent<SpriteRenderer>(out sRenderer))
        {
            sRenderer.sprite = icon;
        }
    }


}
