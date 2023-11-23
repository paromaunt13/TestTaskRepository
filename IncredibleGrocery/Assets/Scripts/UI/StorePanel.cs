using DG.Tweening;
using UnityEngine;

public class StorePanel : MonoBehaviour
{
    [Header("Animation settings")] 
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private const float AnimationDuration = 0.8f;
    private const Ease Ease = DG.Tweening.Ease.InOutExpo;
    
    private CanvasGroup _canvasGroup;
    public CanvasGroup CanvasGroup
    {
        get
        {
            if (_canvasGroup != null) return _canvasGroup;
            _canvasGroup = GetComponent<CanvasGroup>();
            return _canvasGroup;
        }
    }
    public Transform StartPoint => startPoint;
    public Transform EndPoint => endPoint;
    
    public void MovePanel(Transform targetPosition, CanvasGroup canvasGroup)
    {
       canvasGroup.blocksRaycasts = false;
        var positionToMove = transform.position == startPoint.position ? endPoint.position : startPoint.position;
        transform.DOMove(targetPosition.position, AnimationDuration).SetEase(Ease).OnComplete(() =>
        {
            canvasGroup.blocksRaycasts = true;
        });
    }
}