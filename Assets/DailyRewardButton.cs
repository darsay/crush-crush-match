using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardButton : MonoBehaviour
{

    [SerializeField]
    int reward;

    [SerializeField]
    TextMeshProUGUI text;

    string _dailyRewardName = "DailyReward";

    private AdsGameService _adsService;
    Button _button;

    string _dtString;
    AudioSource _audioSource;

    private void Awake()
    {
        _adsService = ServiceLocator.GetService<AdsGameService>();
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();

        _button.onClick.AddListener(WatchAdAndContinue);
    }

    private void Start()
    {

        text.text = $"+{reward}";
        var dt = DateTime.Now;
        _dtString = dt.ToString("dd-MM-yyyy");

        if (PlayerPrefs.HasKey(_dailyRewardName))
        {
            if (PlayerPrefs.GetString(_dailyRewardName).Equals(_dtString))
            {
                gameObject.SetActive(false);
                return;
            }
        }

        PlayerPrefs.SetString(_dailyRewardName, _dtString);

        PrepareAd();

    }

    void PrepareAd()
    {
        if (!_adsService.IsAdReady)
        {
            _button.interactable = false;
            StartCoroutine(WaitForAdReady());
        }
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(WatchAdAndContinue);
    }

    IEnumerator WaitForAdReady()
    {
        while (!_adsService.IsAdReady)
        {
            yield return new WaitForSeconds(0.5f);
        }

        _button.interactable = true;
    }

    async void WatchAdAndContinue()
    {
        if (await ServiceLocator.GetService<AdsGameService>().ShowAd())
        {
            await Task.Delay(500);
            _button.gameObject.SetActive(false);
            _audioSource.Play();
            GameDataEventManager.NotifyOnCoinsChange(ServiceLocator.GetService<GameProgressionService>().GameData.Tokens + reward);
        }
    }
}
