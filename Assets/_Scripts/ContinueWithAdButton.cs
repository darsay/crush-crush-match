using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ContinueWithAdButton : MonoBehaviour
{
    [SerializeField]
    LoseScreen loseScreen;

    private AdsGameService _adsService;
    Button _button;

    private void Awake()
    {
        _adsService = ServiceLocator.GetService<AdsGameService>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(WatchAdAndContinue);
    }

    private void Start()
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
            loseScreen.ContinueAfterAd();
            _button.gameObject.SetActive(false);
        }
    }
}
