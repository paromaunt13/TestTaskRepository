using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private AudioButton mainMenuButton;
    [SerializeField] private AudioButton saveButton;
    [SerializeField] private OnOffButton soundButton;
    [SerializeField] private OnOffButton musicButton;
    
    private void Start()
    {
        soundButton.SetButtonState(AudioManager.Instance.SoundEnabled);
        musicButton.SetButtonState(AudioManager.Instance.MusicEnabled);

        saveButton.Button.onClick.AddListener(SaveSettings);
        mainMenuButton.Button.onClick.AddListener(()=>SceneManager.LoadScene(0));
        
        soundButton.Button.onClick.AddListener(SwitchSoundState);
        musicButton.Button.onClick.AddListener(SwitchMusicState);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void SwitchSoundState()
    {
        AudioManager.Instance.SoundEnabled = !AudioManager.Instance.SoundEnabled;
        soundButton.SetButtonState(AudioManager.Instance.SoundEnabled);
    }
    
    private void SwitchMusicState()
    {
        AudioManager.Instance.MusicEnabled = !AudioManager.Instance.MusicEnabled;
        musicButton.SetButtonState(AudioManager.Instance.MusicEnabled);
    }

    private void SaveSettings()
    {
        AudioManager.Instance.SaveSettings();
        gameObject.SetActive(false);
    }
}