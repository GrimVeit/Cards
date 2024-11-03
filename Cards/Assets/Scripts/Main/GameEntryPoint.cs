using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private UIRootView rootView;
    private Coroutines coroutines;
    public GameEntryPoint()
    {
        coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRootView");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(rootView.gameObject);

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Autorun()
    {
        GlobalGameSettings();

        instance = new GameEntryPoint();
        instance.Run();

    }

    private static void GlobalGameSettings()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Run()
    {
#if UNITY_EDITOR

        //var sceneName = SceneManager.GetActiveScene().name;

        //if (sceneName == Scenes.MAIN_MENU)
        //{
        //    coroutines.StartCoroutine(LoadAndStartMainMenu());
        //    return;
        //}

        //if (sceneName == Scenes.MINI_GAME)
        //{
        //    coroutines.StartCoroutine(LoadAndStartSceneMiniGame());
        //    return;
        //}

        //if (sceneName == Scenes.SLOT_1)
        //{
        //    coroutines.StartCoroutine(LoadAndStartSceneSlots1());
        //    return;
        //}

        //if (sceneName == Scenes.SLOT_2)
        //{
        //    coroutines.StartCoroutine(LoadAndStartSceneSlots2());
        //    return;
        //}

        //if (sceneName == Scenes.SLOT_3)
        //{
        //    coroutines.StartCoroutine(LoadAndStartSceneSlots3());
        //    return;
        //}

        //if (sceneName == Scenes.SLOT_4)
        //{
        //    coroutines.StartCoroutine(LoadAndStartSceneSlots4());
        //    return;
        //}

        //if (sceneName == Scenes.BOOT)
        //{
        //    return;
        //}

#endif

        coroutines.StartCoroutine(LoadAndStartCountryChecker());
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        rootView.SetLoadScreen(0);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.4f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        yield return null;

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMiniGame_Action += () => coroutines.StartCoroutine(LoadAndStartBigCardScene());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartBigCardScene()
    {
        rootView.SetLoadScreen(1);
        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.4f);
        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.BIG_CARD_SCENE);
        yield return null;

        var sceneEntryPoint = Object.FindObjectOfType<BigCardSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartCountryChecker()
    {
        rootView.SetLoadScreen(2);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.COUNTRY_CHECKER);

        yield return new WaitForSeconds(0.2f);

        var sceneEntryPoint = Object.FindObjectOfType<CountryCheckerSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.GoToOther += () => coroutines.StartCoroutine(LoadAndStartOther());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartOther()
    {
        rootView.SetLoadScreen(2);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.OTHER);

        yield return new WaitForSeconds(0.2f);

        var sceneEntryPoint = Object.FindObjectOfType<OtherSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("�������� ����� - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
