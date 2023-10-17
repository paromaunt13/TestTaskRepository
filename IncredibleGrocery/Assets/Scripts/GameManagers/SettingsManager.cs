using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private SoundsData _soundsData;

    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _saveButton;

    [SerializeField] private GameObject _settingsPanel;

    private void Start()
    {
        _settingsButton.onClick.AddListener(ShowSettingsPanel);
        _saveButton.onClick.AddListener(Save);
    }

    private void ShowSettingsPanel()
    {
        _settingsPanel.SetActive(true);
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(Save);
    }

    private void Save()
    {
        AudioManager.Instance.SaveSettings();
        
        _settingsPanel.SetActive(false);
    }
}