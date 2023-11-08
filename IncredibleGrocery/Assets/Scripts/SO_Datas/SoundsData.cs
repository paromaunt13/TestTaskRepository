using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sounds Data", menuName = "Sounds Data")]
public class SoundsData : ScriptableObject
{
    [field: SerializeField] public List<SoundData> SoundsDataList { get; private set; }
}