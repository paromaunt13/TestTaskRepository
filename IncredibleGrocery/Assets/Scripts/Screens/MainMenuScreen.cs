using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private AudioButton settingsScreenButton;
    [SerializeField] private AudioButton continueButton;
    [SerializeField] private AudioButton newGameButton;
    [SerializeField] private AudioButton quitButton;
    [SerializeField] private int gameSceneIndex;

    private void Start()
    {
        settingsScreenButton.Button.onClick.AddListener(() =>
        {
            settingsScreen.SetActive(true);
        });
        continueButton.Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(gameSceneIndex);
        });
        newGameButton.Button.onClick.AddListener(StartNewGame);
        quitButton.Button.onClick.AddListener(QuitGame);

        continueButton.Button.interactable = !PersistentDataManager.FirstLaunch;
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

    private void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        PersistentDataManager.FirstLaunch = false;
        SceneManager.LoadScene(gameSceneIndex);
    }
}