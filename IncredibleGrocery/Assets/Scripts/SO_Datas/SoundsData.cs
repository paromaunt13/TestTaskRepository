using UnityEngine;

[CreateAssetMenu(fileName = "Sounds Data", menuName = "Sounds Data")]
public class SoundsData : ScriptableObject
{
    [field: SerializeField] public AudioClip ButtonClickSound { get; private set; }
    [field: SerializeField] public AudioClip BubbleAppearSound { get; private set; }
    [field: SerializeField] public AudioClip BubbleDisappearSound { get; private set; }
    [field: SerializeField] public AudioClip ProductSelectSound { get; private set; }
    [field: SerializeField] public AudioClip MoneyIncomeSound { get; private set; }
}