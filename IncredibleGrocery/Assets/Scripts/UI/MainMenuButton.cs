using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : InterfaceButton
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        base.OnClick();
        SceneManager.LoadScene(0);
    }
}