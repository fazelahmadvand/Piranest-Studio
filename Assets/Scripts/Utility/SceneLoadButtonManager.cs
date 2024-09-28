using UnityEngine;
using UnityEngine.UI;
using Piranest;

public class SceneLoadButtonHandler : MonoBehaviour
{
    public Button loadSceneButton;

    void Start()
    {
        loadSceneButton.onClick.AddListener(OnLoadSceneButtonClicked);
    }

    private void OnLoadSceneButtonClicked()
    {
        SceneLoader.LoadScene(SceneName.MainMenu);
    }
}
