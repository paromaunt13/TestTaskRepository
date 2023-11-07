using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button saveButton;

    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        settingsButton.onClick.AddListener(ShowSettingsPanel);
        saveButton.onClick.AddListener(Save);
    }

    private void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    private void Save()
    {
        AudioManager.Instance.SaveSettings();

        settingsPanel.SetActive(false);
    }
}