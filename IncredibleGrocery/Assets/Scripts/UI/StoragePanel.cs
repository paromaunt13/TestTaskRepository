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

    private Vector3 _currentPosition;

    private void Start()
    {
        transform.position = startPosition.position;

        OrderManagerView.OnOrderViewCreated += MovePanel;
    }

    private void OnDestroy()
    {
        OrderManagerView.OnOrderViewCreated -= MovePanel;
    }

    public void MovePanel()
    {
        var positionToMove = 
            transform.position == startPosition.position ? endPosition.position : startPosition.position;

        transform.DOMove(positionToMove, animationDuration).SetEase(ease);
    }
}