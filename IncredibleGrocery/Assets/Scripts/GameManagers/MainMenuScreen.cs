using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private int gameSceneIndex;

    private void Start()
    {
        continueButton.onClick.AddListener(ContinueGame);
        newGameButton.onClick.AddListener(StartNewGame);
        quitButton.onClick.AddListener(QuitGame);

        CheckContinueButtonState();
    }

    private void CheckContinueButtonState()
    {
        continueButton.interactable = !PersistentDataManager.FirstLaunch;
    }
    private void ContinueGame()
    {
        SceneManager.LoadScene(gameSceneIndex);
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