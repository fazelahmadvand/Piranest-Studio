using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Piranest
{
    public class SceneLoader
    {

        public static void LoadScene(SceneName sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName.ToString(), loadSceneMode);
        }

        public static IEnumerator LoadScene(SceneName sceneName, Action OnLoad)
        {
            yield return null;
        }

    }

    public enum SceneName
    {
        Loading = 0,
        MainMenu = 1,
        ARMultipleObjects_Puzzles = 2,
    }
}
