using TMPro;
using UnityEngine;

public class SettingsButton : InterfaceButton
{
    [SerializeField] protected Sprite enabledImage;
    [SerializeField] protected Sprite disabledImage;
    [SerializeField] protected TMP_Text buttonText;

    private const string OnText = "ON";
    private const string OffText = "OFF";

    private bool _enabled;

    protected void UpdateButtonView(bool enabled)
    {
        switch (enabled)
        {
            case true:
                _button.image.sprite = enabledImage;
                buttonText.text = OnText;
                break;
            case false:
                _button.image.sprite = disabledImage;
                buttonText.text = OffText;
                break;
        }
    }
}