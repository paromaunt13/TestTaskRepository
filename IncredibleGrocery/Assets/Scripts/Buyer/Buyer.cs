using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    [Header("Moving settings")]
    [SerializeField] private GameObject buyerWalking;
    [SerializeField] private float stepInterval;
    [SerializeField] private float stepDuration;
    [SerializeField] private float moveDuration;
    [SerializeField] private Ease ease;

    private Vector3[] _pathToMove;
    private Sequence _sequence;

    private int _stepCount;
    private bool _movingForward;

    public Vector3[] OrderPathPoints { get; set; }
    public Vector3[] ExitPathPoints { get; set;}

    private void Start()
    {
        _stepCount = (int)(moveDuration / stepInterval);
        _movingForward = true;
        SellManager.OnOrderComplete += Move;

        Move(_movingForward);
    }
    
    private void OnDestroy()
    {
        SellManager.OnOrderComplete -= Move;
    }

    private void Move(bool movingForward)
    {
        if (_movingForward && movingForward)
        {
            _pathToMove = OrderPathPoints;
            _movingForward = false;
        }
        else
        {
            _pathToMove = ExitPathPoints;
            Flip();
        }
        
        _sequence.Append(buyerWalking.transform.DOLocalMoveY(buyerWalking.transform.position.y, stepDuration)
                .SetLoops(_stepCount, LoopType.Yoyo).SetEase(ease))
            .Join(transform.DOPath(_pathToMove, moveDuration)
                .SetEase(ease)).OnComplete(() => _sequence.Kill());
    }

    [ContextMenu("Flip")]
    private void Flip()
    {
        var localScale = buyerWalking.transform.localScale;
        localScale.x *= -1f;
        buyerWalking.transform.localScale = localScale;
    }
    
    public void LeaveStore()
    {
        Move(_movingForward);
    }
    
    public Order MakeOrder()
    {
        return new Order {Products = new List<ProductItem>()};
    }
}