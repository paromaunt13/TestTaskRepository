using UnityEngine.UI;

public class MusicButton : SettingsButton
{
    private bool _enabled;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);

        _enabled = AudioManager.Instance.MusicEnabled;

        UpdateButtonView(_enabled);
    }

    protected override void OnClick()
    {
        SwitchState();

        AudioManager.Instance.SwitchMusicState(_enabled);

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