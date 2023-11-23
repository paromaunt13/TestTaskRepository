using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AudioButton : MonoBehaviour
{
    private Button _button;
    public Button Button
    {
        get
        {
            if (_button != null) return _button;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySound(SoundType.ButtonClickSound);
            });
            return _button;
        }
    }
}