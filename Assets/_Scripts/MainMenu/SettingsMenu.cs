using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] RectTransform settingsPopUp;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixer audioMixer;
    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OpenSettings()
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOFade(1, 0.5f);
        BoardEventManager.NotifyOnInputLocked(true);

        settingsPopUp.DOScale(1, 0.5f).SetEase(Ease.OutElastic);
    }

    public void CloseSettings()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0, 0.5f);
        BoardEventManager.NotifyOnInputLocked(false);

        settingsPopUp.localScale = Vector2.zero;
    }

    public void SetMusicVolume()
    {


        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
    }
}
