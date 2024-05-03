using System;
using System.Collections;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;

public static class SceneLoader
{

    //Courotines need an instance that is MonoBehaviour, so we need to simulate it, a dummy object
    private class LoadingMonoBehaviour : MonoBehaviour { }

    public enum Scene
    {
        MainMenu,
        LoadingScreen,
        GamesMenu,
        StoryMode
    }


    // delegate Toast store the action we are going to execute
    // Action is async delegate ThreadStaticAttribute returns void 

    private static Action onLoaderCallback;

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        // Waits a mininum time
        yield return new WaitForSecondsRealtime(Utils.LoadingScreenMinimumTime);
        //yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());


        while (!asyncOperation.isDone)
        {
            //Waits for next frame until it is loaded
            yield return null;
        }

    }

    public static void Load(Scene scene)
    {
        // TODO there is a small delay caused by the async opetation

        // Associates the calling of the loadscene inside the delegate action
        // Set the loader callback action to load the target scene
        // here is where it is preloaded the real scene, while it show cases the loading
        onLoaderCallback = () =>
        {
            // Uses the private function to call the scene async
            //First needs to start a courotine, which is ReadOnly called by a monobehaviour class
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
            LoadSceneAsync(scene);
        };

        // Load the loading screen
        SceneManager.LoadScene(Scene.LoadingScreen.ToString());
    }

    public static void SceneLoaderCallback()
    {
        // Trigered after the first update which lets the screen refresh
        // Execute the loader callback action which will load the target scene
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

}
