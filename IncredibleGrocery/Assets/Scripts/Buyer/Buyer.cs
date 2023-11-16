using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class Buyer : MonoBehaviour
{
    [Header("Moving settings")]
    [SerializeField] private GameObject buyerWalking;
    [SerializeField] private float stepDuration;
    [SerializeField] private float moveDurationToEntrance;
    [SerializeField] private float moveDurationToQueuePoint;
    [SerializeField] private float moveDurationInQueue;
    [SerializeField] private float moveDurationToExit;
    [SerializeField] private Ease ease;

    private Sequence _sequence;

    private SortingGroup _sortingGroup;
    private int _currentLayerOrder;
    private bool _isInQueue; 
    
    public List<Transform> OrderPathPoints { get; set; }
    public Transform ExitPoint { get; set; }
    public Transform EntrancePoint { get; set; }
    public int CurrentIndexInQueue { get; set; }

    public Action OnPointReached;

    private void Start()
    {
        MoveToEntrance();
        _sortingGroup = GetComponent<SortingGroup>();
        _sortingGroup.sortingOrder = -1;
    }
    
    public void MoveInQueue(Transform positionToMove)
    {
        var moveDuration = _isInQueue ? moveDurationInQueue : moveDurationToQueuePoint;

        transform.DOPlay();
        
        _sequence.Append(buyerWalking.transform.DOLocalMoveY(buyerWalking.transform.position.y, stepDuration)
                .SetLoops(-1, LoopType.Yoyo).SetEase(ease))
            .Join(transform.DOMove(positionToMove.position, moveDuration).SetEase(ease).OnComplete(() =>
            {
                _isInQueue = true;
                OnPointReached?.Invoke();
                buyerWalking.transform.DOPause();
            }));
    }

    private void MoveToEntrance()
    {
        _sequence.Append(buyerWalking.transform.DOLocalMoveY(buyerWalking.transform.position.y, stepDuration)
                .SetLoops(-1, LoopType.Yoyo).SetEase(ease))
            .Join(transform.DOMove(EntrancePoint.position, moveDurationToEntrance).SetEase(ease).OnComplete(() =>
            {
                _sortingGroup.sortingOrder = 0;
                MoveInQueue(OrderPathPoints[CurrentIndexInQueue]);
            }));
    }

    [ContextMenu("Flip")]
    private void Flip()
    {
        var localScale = buyerWalking.transform.localScale;
        localScale.x *= -1f;
        buyerWalking.transform.localScale = localScale;
    }
    
    private void MoveToExit()
    {
        var position = transform.position;
        _sequence.Append(buyerWalking.transform.DOLocalMoveY(buyerWalking.transform.position.y, stepDuration)
                .SetLoops(-1, LoopType.Yoyo).SetEase(ease))
            .Join(transform.DOMove(new Vector3(position.x, position.y + 1f, position.z), 0.6f).OnComplete(() =>
            {
                Flip();
                transform.DOMove(EntrancePoint.position, moveDurationToExit).SetEase(ease).OnComplete(() =>
                {
                    _sortingGroup.sortingOrder = -1;
                    transform.DOMove(ExitPoint.position, moveDurationToExit).SetEase(ease).OnComplete(() => buyerWalking.transform.DOKill());
                });
            }));
    }
    
    public Order MakeOrder()
    {
        return new Order {Products = new List<ProductItem>()};
    }
    
    public void LeaveStore()
    {
        MoveToExit();
    }
}
