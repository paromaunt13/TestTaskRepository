using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] protected SoundsData soundsData;

    private Button _button;
    public Button Button
    {
        get
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
                _button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySound(soundsData.ButtonClickSound);
                });
            }
            return _button;
        }
    }
}