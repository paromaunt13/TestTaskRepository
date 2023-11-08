using UnityEngine;
using UnityEngine.UI;

public class InterfaceButton : MonoBehaviour
{
    [SerializeField] protected SoundsData soundsData;

    public Button Button;

    private void Awake()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OnClick);
    }

    protected  void OnClick() => AudioManager.Instance.PlaySound(soundsData.ButtonClickSound);
}