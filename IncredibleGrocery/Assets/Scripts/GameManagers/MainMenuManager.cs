using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private int _gameSceneIndex;

    private void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _quitButton.onClick.RemoveListener(QuitGame);
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
        SceneManager.LoadScene(_gameSceneIndex);
    }
}