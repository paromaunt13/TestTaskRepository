using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    
    [SerializeField] private Button openSettingsPanelButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private OnOffButton soundButton;
    [SerializeField] private OnOffButton musicButton;
    
    private void Start()
    {
        soundButton.SetButtonState(AudioManager.Instance.SoundEnabled);
        musicButton.SetButtonState(AudioManager.Instance.MusicEnabled);

        soundButton.Button.onClick.AddListener(SwitchSoundState);
        musicButton.Button.onClick.AddListener(SwitchMusicState);
        
         openSettingsPanelButton.onClick.AddListener(() => settingsPanel.SetActive(true));
         saveButton.onClick.AddListener(SaveSettings);
         mainMenuButton.onClick.AddListener(()=>SceneManager.LoadScene(0));
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
        settingsPanel.SetActive(false);
    }
}