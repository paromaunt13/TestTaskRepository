using UnityEngine.UI;

public class SoundButton : SettingsButton
{
    private bool _enabled;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);

        _enabled = AudioManager.Instance.SoundEnabled;

        UpdateButtonView(_enabled);
    }

    protected override void OnClick()
    {
        SwitchState();
        
        AudioManager.Instance.SwitchSoundState(_enabled);

        base.OnClick();
    }

    private void SwitchState()
    {
        _enabled = _enabled switch
        {
            true => false,
            false => true
        };
        
        UpdateButtonView(_enabled);
    }
}