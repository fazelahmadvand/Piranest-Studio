using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Piranest
{
    public class SceneLoader
    {

        public static void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName, loadSceneMode);
        }

        public static IEnumerator LoadScene(string sceneName, Action OnLoad)
        {
            yield return null;
        }

    }
}
