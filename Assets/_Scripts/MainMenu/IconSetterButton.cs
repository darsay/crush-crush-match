using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSetterButton : MonoBehaviour
{
    public int _iconId;
    public bool _isPlayer;

    Image _image;
    Button _button;
    AudioSource _audioSource;


    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SetIcon);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SetIcon);
    }

    public void ButtonInitializer(bool isPlayer, int iconId)
    {
        _isPlayer = isPlayer;
        _iconId = iconId;

        ServiceLocator.GetService<IconLoaderService>().LoadIcon(_iconId, (icon) => _image.sprite = icon);
    }

    void SetIcon()
    {
        if (_isPlayer)
        {
            GameDataEventManager.NotifyOnPlayerIconChange(_iconId);
        }
        else
        {
            GameDataEventManager.NotifyOnCrushIconChange(_iconId);
        }

        _audioSource.Play();
        
    }
}
