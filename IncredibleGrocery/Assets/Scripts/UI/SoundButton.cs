using UnityEngine.UI;

public class SoundButton : SettingsButton
{
    private ButtonState _buttonState;

    private bool _enabled;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);

        _enabled = AudioManager.Instance.SoundEnabled;

        SetState();
    }

    protected override void OnClick()
    {
        SwitchState();
        AudioManager.Instance.SwitchSoundState(_enabled);
        
        base.OnClick();
    }

    private void Enable()
    {
        _buttonState = ButtonState.Enabled;
        _enabled = true;
        UpdateButtonView(_buttonState);
    }

    private void Disable()
    {
        _buttonState = ButtonState.Disabled;
        _enabled = false;
        UpdateButtonView(_buttonState);
    }

    private void SetState()
    {
        if (_enabled)
        {
            _buttonState = ButtonState.Enabled;
            UpdateButtonView(_buttonState);
        }
        else
        {
            _buttonState = ButtonState.Disabled;
            UpdateButtonView(_buttonState);
        }
    }

    private void SwitchState()
    {
        if (_buttonState == ButtonState.Enabled)
        {
            Disable();
        }
        else if (_buttonState == ButtonState.Disabled)
        {
            Enable();
        }
    }
}