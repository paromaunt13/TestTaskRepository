using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnOffButton : AudioButton
{
    [SerializeField] protected Sprite enabledImage;
    [SerializeField] protected Sprite disabledImage;
    [SerializeField] protected TMP_Text buttonText;
    
    private const string OnText = "ON";
    private const string OffText = "OFF";

    private bool _isEnabled;
    
    public void SetButtonState(bool isEnabled)
    {
        if (_isEnabled == isEnabled) return;
        _isEnabled = isEnabled;
        Button.image.sprite = _isEnabled ? enabledImage : disabledImage;
        buttonText.text = _isEnabled ? OnText : OffText;
    }
}