using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : AudioButton
{
    private void Start()
    {
        Button.onClick.AddListener(()=> { SceneManager.LoadScene(0); });
    }
}