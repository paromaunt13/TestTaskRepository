using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private int gameSceneIndex;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
#if PLATFORM_ANDROID
        Application.Quit();
#endif
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }

    private void StartGame()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
}