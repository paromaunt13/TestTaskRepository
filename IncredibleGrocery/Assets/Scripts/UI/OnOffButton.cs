using TMPro;
using UnityEngine;

public class OnOffButton : AudioButton
{
    [SerializeField] protected Sprite enabledImage;
    [SerializeField] protected Sprite disabledImage;
    [SerializeField] protected TMP_Text buttonText;
    
    private const string OnText = "ON";
    private const string OffText = "OFF";

    public void SetButtonState(bool isEnabled)
    {
        Button.image.sprite = isEnabled ? enabledImage : disabledImage;
        buttonText.text = isEnabled ? OnText : OffText;
    }
}