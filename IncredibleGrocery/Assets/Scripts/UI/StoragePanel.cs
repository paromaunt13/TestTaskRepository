using DG.Tweening;
using UnityEngine;

public class StoragePanel : MonoBehaviour
{
    [Header("Transform settings")]   
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;

    [Header("Animation settings")]
    [SerializeField] private float _animationDuration;
    [SerializeField] private Ease _ease;

    private void Start()
    {
        transform.position = _startPosition.position;

        EventBus.OnOrderComplete += HidePanel;
        EventBus.OnOrderViewCreated += ShowPanel;      
    }

    private void OnDisable()
    {
       EventBus.OnOrderComplete -= HidePanel;
       EventBus.OnOrderViewCreated -= ShowPanel;
    }

    private void ShowPanel()
    {
        transform.DOMove(_endPosition.position, _animationDuration).SetEase(_ease);      
    }

    public void HidePanel()
    {
        transform.DOMove(_startPosition.position, _animationDuration).SetEase(_ease);
    }
}