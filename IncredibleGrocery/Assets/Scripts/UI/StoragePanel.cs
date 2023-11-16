using DG.Tweening;
using UnityEngine;

public class StoragePanel : MonoBehaviour
{
    [Header("Transform settings")] 
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    [Header("Animation settings")] 
    [SerializeField] private float animationDuration;
    [SerializeField] private Ease ease;

    private void Start()
    {
        transform.position = endPosition.position;
        //OrderView.OnOrderViewCreated += MovePanel;
        //OrderZone.OnBuyerLeaved += MovePanel;
    }

    private void OnDestroy()
    {
        //OrderView.OnOrderViewCreated -= MovePanel;
        //OrderZone.OnBuyerLeaved -= MovePanel;
    }

    public void MovePanel()
    {
        var positionToMove = transform.position == startPosition.position ? endPosition.position : startPosition.position;
        transform.DOMove(positionToMove, animationDuration).SetEase(ease);
    }
}