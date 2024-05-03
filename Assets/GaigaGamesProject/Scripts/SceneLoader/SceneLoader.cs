using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;

public static class SceneLoader
{

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

    private static IEnumerator LoadindScreenMininumTimeOnScreen()
    {
        Debug.Log("Waiting 1 second");
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("DONE");
    }

    public static void Load(Scene scene)
    {
        // Associates the calling of the loadscene inside the delegate action
        // Set the loader callback action to load the target scene
        // here is where it is preloaded the real scene, while it show cases the loading
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
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
