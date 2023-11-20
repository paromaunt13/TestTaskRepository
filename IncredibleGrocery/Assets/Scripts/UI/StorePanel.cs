using DG.Tweening;
using UnityEngine;

public class StorePanel : MonoBehaviour
{
    [Header("Animation settings")] 
    [SerializeField] private float animationDuration;
    [SerializeField] private Ease ease;

    public Transform StartPoint { get; set; }
    public Transform EndPoint { get; set; }

    public void MovePanel()
    {
        var positionToMove = transform.position == StartPoint.position ? EndPoint.position : StartPoint.position;
        transform.DOMove(positionToMove, animationDuration).SetEase(ease);
    }
}