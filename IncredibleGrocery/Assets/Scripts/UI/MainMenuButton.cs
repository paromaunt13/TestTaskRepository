using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : InterfaceButton
{
    private void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        base.OnClick();
        SceneManager.LoadScene(0);
    }
}