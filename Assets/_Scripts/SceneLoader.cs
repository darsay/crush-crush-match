using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] CanvasGroup canvasGroup;

    [SerializeField] float fadeTime;

    [SerializeField] float waitingTime;

    [SerializeField] LoadingSceneTip[] tips;

    [SerializeField] TextMeshProUGUI tipText;
    [SerializeField] Image tipImage;

    private void Awake()
    {
        if(Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int id)
    {
        StartCoroutine(LoadSceneAsync(id));
    }

    IEnumerator LoadSceneAsync(int id)
    {

        SetTip();

        float timeToLoad = 0;

        canvasGroup.DOFade(1, fadeTime);
        canvasGroup.blocksRaycasts = true;

        yield return new WaitForSeconds(fadeTime);

        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(id);



        while (!loadingOperation.isDone)
        {
            timeToLoad += Time.deltaTime;
            yield return null;
        }

        if (waitingTime > timeToLoad)
        {
            yield return new WaitForSeconds(waitingTime - timeToLoad);
        }

        canvasGroup.DOFade(0, fadeTime).OnComplete(()=> canvasGroup.blocksRaycasts = false);
        
    }

    private void SetTip()
    {
        int r = UnityEngine.Random.Range(0, tips.Length);

        var tip = tips[r];

        tipText.text = tip.Description;
        tipImage.sprite = tip.Sprite;
    }
}
