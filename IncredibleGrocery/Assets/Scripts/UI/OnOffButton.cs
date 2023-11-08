using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnOffButton : InterfaceButton
{
    [SerializeField] protected Sprite enabledImage;
    [SerializeField] protected Sprite disabledImage;
    [SerializeField] protected TMP_Text buttonText;
    
    private const string OnText = "ON";
    private const string OffText = "OFF";

    private void UpdateButtonView(bool isEnabled)
    {
        switch (isEnabled)
        {
            case true:
                Button.image.sprite = enabledImage;
                buttonText.text = OnText;
                break;
            case false:
                Button.image.sprite = disabledImage;
                buttonText.text = OffText;
                break;
        }
    }
    
    public void SwitchButtonState(bool isEnabled)
    {
        isEnabled = isEnabled switch
        {
            true => false,
            false => true
        };
        
        UpdateButtonView(isEnabled);
    }
}