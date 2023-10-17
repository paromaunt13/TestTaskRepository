using TMPro;
using UnityEngine;

public class SettingsButton : InterfaceButton
{  
    [SerializeField] protected Sprite _enabledImage;
    [SerializeField] protected Sprite _disabledImage;
    [SerializeField] protected TMP_Text _buttonText;

    protected const string OnText = "ON";
    protected const string OffText = "OFF";

    protected override void OnClick()
    {
        base.OnClick();
    }

    protected void UpdateButtonView(ButtonState buttonState)
    {
        switch (buttonState)
        {
            case ButtonState.Enabled:
                _button.image.sprite = _enabledImage;
                _buttonText.text = OnText;
                break;
            case ButtonState.Disabled:
                _button.image.sprite = _disabledImage;
                _buttonText.text = OffText;
                break;
        }    
    }
}