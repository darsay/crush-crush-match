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

    string _musicSliderName = "MusicVolume";
    string _sfxSliderName = "SFXVolume";

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(_musicSliderName))
        {
            musicSlider.value = PlayerPrefs.GetFloat(_musicSliderName);
            sfxSlider.value = PlayerPrefs.GetFloat(_sfxSliderName);
            PlayerPrefs.SetFloat(_musicSliderName, musicSlider.value);
            PlayerPrefs.SetFloat(_sfxSliderName, sfxSlider.value);
        }
        else
        {
            PlayerPrefs.SetFloat(_musicSliderName, 1);
            PlayerPrefs.SetFloat(_sfxSliderName, 1);
        }
        
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
        audioMixer.SetFloat(_musicSliderName, Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat(_musicSliderName, musicSlider.value);
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat(_sfxSliderName, Mathf.Log10(sfxSlider.value) * 20);
        PlayerPrefs.SetFloat(_sfxSliderName, sfxSlider.value);
    }
}
