using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Buyer : MonoBehaviour
{
    [SerializeField] private float minWaitingTime;
    [SerializeField] private float maxWaitingTime;
    [Header("Moving settings")]
    [SerializeField] private GameObject buyerWalking;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float stepDuration;
    [SerializeField] private float distanceBetweenBuyers;
    [SerializeField] private float moveDurationToEntrance;
    [SerializeField] private float moveDurationInPath;
    [SerializeField] private float moveDurationToExit;
    [SerializeField] private Ease ease;

    private string _buyerTag;
    private BoxCollider2D _collider;
    
    private SortingGroup _sortingGroup;
    private int _currentLayerOrder;
    
    private bool _ableToCheck;
    private bool _isOnOrderPoint;

    private float _waitingTime;
    
    private Sequence _sequence;

    private Vector3[] _pathPoints;
    private Transform _exitPoint;
    private Transform _entrancePoint;
    public Action<float> OnPointReached;

    private void Start()
    {
        _collider = gameObject.GetComponent<BoxCollider2D>();
        _sortingGroup = GetComponent<SortingGroup>();
        _sortingGroup.sortingOrder = -1;
        _buyerTag = gameObject.tag;
        _ableToCheck = false;
        _waitingTime = Random.Range(minWaitingTime, maxWaitingTime);
        MoveToEntrance();
    }

    private void OnDestroy()
    {
        transform.DOKill();
        buyerWalking.transform.DOKill();
    }

    private void Update()
    {
        if (!_ableToCheck) return;
        if (IsBuyerInFront() || _isOnOrderPoint)
        {
            transform.DOPause();
            buyerWalking.transform.DOPause();
            OnPointReached?.Invoke(_waitingTime);
        }
        else
        {
            transform.DOPlay();
            buyerWalking.transform.DOPlay();
        }
    }
    
    private bool IsBuyerInFront()
    {
        var hit = Physics2D.Raycast(raycastPoint.position, Vector2.right, distanceBetweenBuyers);
        return hit.collider is not null && hit.collider.CompareTag(_buyerTag) && hit.collider.isActiveAndEnabled;
    }
    
    private void MoveToEntrance()
    {
        _sequence.Append(buyerWalking.transform.DOLocalMoveY(buyerWalking.transform.position.y, stepDuration)
                .SetLoops(-1, LoopType.Yoyo).SetEase(ease))
                .Join(transform.DOMove(_entrancePoint.position, moveDurationToEntrance).SetEase(ease).OnComplete(() =>
                { 
                    _ableToCheck = true;
                    _sortingGroup.sortingOrder = 0;
                    MoveInPath();
            }));
    }

    private void MoveInPath()
    {
        _sequence.Append(buyerWalking.transform.DOLocalMoveY(buyerWalking.transform.position.y, stepDuration)
                .SetLoops(-1, LoopType.Yoyo).SetEase(ease))
                .Join(transform.DOPath(_pathPoints, moveDurationInPath).SetEase(ease).OnComplete(() =>
            {
                _isOnOrderPoint = true;
                buyerWalking.transform.DOPause();
                OnPointReached?.Invoke(_waitingTime);
            }));
    }
    
    private void MoveToExit()
    {
        _collider.enabled = false;
        _ableToCheck = false;
        
        var position = transform.position;
        
        _sequence.Append(buyerWalking.transform.DOLocalMoveY(buyerWalking.transform.position.y, stepDuration)
                .SetLoops(-1, LoopType.Yoyo).SetEase(ease))
                .Join(transform.DOMove(new Vector3(position.x, position.y + 1f, position.z), moveDurationToExit/3).OnComplete(() =>
            {
                Flip();
                transform.DOMove(_entrancePoint.position, moveDurationToExit).SetEase(ease).OnComplete(() =>
                {
                    _collider.enabled = true;
                    _sortingGroup.sortingOrder = -1;
                    transform.DOMove(_exitPoint.position, moveDurationToExit).SetEase(ease);
                });
            }));
    }
    
    [ContextMenu("Flip")]
    private void Flip()
    {
        var localScale = buyerWalking.transform.localScale;
        localScale.x *= -1f;
        buyerWalking.transform.localScale = localScale;
    }

    public void SetBuyerMoveData(Vector3[] pathPoints, Transform entrancePoint, Transform exitPoint)
    {
        _pathPoints = pathPoints;
        _entrancePoint = entrancePoint;
        _exitPoint = exitPoint;
    }
    
    public void LeaveStore() => MoveToExit();
}