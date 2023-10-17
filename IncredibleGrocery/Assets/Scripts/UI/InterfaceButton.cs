using UnityEngine;
using UnityEngine.UI;

public class InterfaceButton : MonoBehaviour
{
    [SerializeField] protected SoundsData _soundsData;

    protected Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {
        AudioManager.Instance.PlaySound(_soundsData.ButtonClickSound);
    }
}
