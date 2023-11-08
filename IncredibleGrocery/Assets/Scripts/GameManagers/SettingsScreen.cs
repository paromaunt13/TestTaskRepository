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

    private bool _soundEnabled;
    private bool _musicEnabled;
    
    private void Start()
    {
        _soundEnabled = AudioManager.Instance.SoundEnabled;
        _musicEnabled = AudioManager.Instance.MusicEnabled;
        
        soundButton.SwitchButtonState(_soundEnabled);
        musicButton.SwitchButtonState(_musicEnabled);

        soundButton.Button.onClick.AddListener(SwitchSoundState);
        musicButton.Button.onClick.AddListener(SwitchMusicState);
        
         openSettingsPanelButton.onClick.AddListener(() => settingsPanel.SetActive(true));
         saveButton.onClick.AddListener(SaveSettings);
         mainMenuButton.onClick.AddListener(()=>SceneManager.LoadScene(0));
    }

    private void SwitchSoundState()
    {
        _soundEnabled = SwitchState(_soundEnabled);
        soundButton.SwitchButtonState(_soundEnabled);
        AudioManager.Instance.SwitchSoundState(_soundEnabled);
    }
    
    private void SwitchMusicState()
    {
        _musicEnabled = SwitchState(_musicEnabled);
        musicButton.SwitchButtonState(_musicEnabled);
        AudioManager.Instance.SwitchMusicState(_musicEnabled);
    }

    private void SaveSettings()
    {
        AudioManager.Instance.SaveSettings();

        settingsPanel.SetActive(false);
    }
    
    private bool SwitchState(bool isEnabled)
    {
        isEnabled = isEnabled switch
        {
            true => false,
            false => true
        };

        return isEnabled;
    }
}