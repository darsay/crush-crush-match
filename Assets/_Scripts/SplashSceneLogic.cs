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
        Application.targetFrameRate = 60;

        string environmentId = IsDevBuild ? "development" : "production";

        ServicesInitializer servicesInitializer = new ServicesInitializer(environmentId);

        //create services
        GameConfigService gameConfig = new GameConfigService();
        AuthGameService authService = new AuthGameService();
        GameProgressionService gameProgression = new GameProgressionService();
        RemoteConfigGameService remoteConfig = new RemoteConfigGameService();
        AnalyticsGameService analyticsService = new AnalyticsGameService();
        AdsGameService adsService = new AdsGameService("4928655", "Rewarded_Android");
        IconLoaderService iconLoaderService = new IconLoaderService();
        LevelsGameService levelsService = new LevelsGameService();

        //register services
        ServiceLocator.RegisterService(gameConfig);
        ServiceLocator.RegisterService(authService);
        ServiceLocator.RegisterService(gameProgression);
        ServiceLocator.RegisterService(remoteConfig);
        ServiceLocator.RegisterService(analyticsService);
        ServiceLocator.RegisterService(iconLoaderService);
        ServiceLocator.RegisterService(adsService);
        ServiceLocator.RegisterService(levelsService);

        //initialize services
        await servicesInitializer.Initialize();
        await authService.Initialize();
        await remoteConfig.Initialize();
        await analyticsService.Initialize();

        await adsService.Initialize(Application.isEditor);

        gameConfig.Initialize(remoteConfig);
        gameProgression.LoadGameData(gameConfig);
        levelsService.Initialize();

        while (!animationSceneLoader.IsAnimationFinished)
        {
            await Task.Delay(500);
        }

        SceneManager.LoadScene(animationSceneLoader.SceneToLoad);
    }

}
