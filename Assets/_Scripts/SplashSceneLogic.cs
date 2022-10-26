using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneLogic : MonoBehaviour
{
    [SerializeField]
    private bool IsDevBuild = true;

    [SerializeField]
    AnimationSceneLoader animationSceneLoader;

    [SerializeField]
    Transform policyPopUp;


    private TaskCompletionSource<bool> _cancellationTaskSource;

    void Start()
    {
        _cancellationTaskSource = new();
        LoadServicesCancellable().ContinueWith(task =>
                Debug.LogException(task.Exception),
            TaskContinuationOptions.OnlyOnFaulted);
    }

    private void OnDestroy()
    {
        _cancellationTaskSource.SetResult(true);
    }

    private async Task LoadServicesCancellable()
    {
        await Task.WhenAny(LoadServices(), _cancellationTaskSource.Task);
    }

    private async Task LoadServices()
    {
        PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60;

        string environmentId = IsDevBuild ? "development" : "production";

        ServicesInitializer servicesInitializer = new ServicesInitializer(environmentId);

        //create services
        GameConfigService gameConfig = new GameConfigService();
        AuthGameService authService = new AuthGameService();
        GameProgressionService gameProgressionService = new GameProgressionService();
        RemoteConfigGameService remoteConfig = new RemoteConfigGameService();
        AnalyticsGameService analyticsService = new AnalyticsGameService();
        AdsGameService adsService = new AdsGameService("4928655", "Rewarded_Android");
        IconLoaderService iconLoaderService = new IconLoaderService();
        LevelsGameService levelsService = new LevelsGameService();
        IIAPGameService iAPService = new UnityIAPGameService();

        //register services
        ServiceLocator.RegisterService(gameConfig);
        ServiceLocator.RegisterService(authService);
        ServiceLocator.RegisterService(gameProgressionService);
        ServiceLocator.RegisterService(remoteConfig);
        ServiceLocator.RegisterService(analyticsService);
        ServiceLocator.RegisterService(iconLoaderService);
        ServiceLocator.RegisterService(adsService);
        ServiceLocator.RegisterService(levelsService);
        ServiceLocator.RegisterService(iAPService);

        //initialize services
        await servicesInitializer.Initialize();
        await authService.Initialize();
        await remoteConfig.Initialize();
        await analyticsService.Initialize();

        await adsService.Initialize(Application.isEditor);
        await iAPService.Initialize(new Dictionary<string, string>()
        {
            ["skinspack"] = "skinspack",
            ["skinspack02"] = "skinspack02"
        });

        gameConfig.Initialize(remoteConfig);
        await gameProgressionService.Initialize(gameConfig, PlayerPrefs.GetInt("CloudProgression", 0));
        levelsService.Initialize();


        while (!animationSceneLoader.IsAnimationFinished)
        {
            await Task.Delay(500);
        }

        if (PlayerPrefs.GetInt("PolicyAgree", 0) == 0)
        {
            policyPopUp.DOScale(1, 0.5f).SetEase(Ease.OutElastic);
        }
        else
        {
            SceneManager.LoadScene(animationSceneLoader.SceneToLoad);
        }
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("PolicyAgree", 1);
        SceneManager.LoadScene(animationSceneLoader.SceneToLoad);
    }

}
