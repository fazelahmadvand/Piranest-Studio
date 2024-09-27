using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Piranest
{

    public static class SceneLoader
    {
        public static void LoadScene(SceneName sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName.ToString(), mode);
        }







    }

    public enum SceneName
    {
        Loading = 0,
        MainMenu = 1,

    }

}
