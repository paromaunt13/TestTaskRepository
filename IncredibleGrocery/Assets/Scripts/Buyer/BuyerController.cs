using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BuyerController : MonoBehaviour
{
    [Header("Objects to move")] 
    [SerializeField] private GameObject buyerWalking;

    [Header("Path points")] 
    [SerializeField] private Transform entrancePoint;
    [SerializeField] private Transform orderPoint;
    [SerializeField] private Transform exitPoint;

    [Header("Moving settings")] 
    [SerializeField] private float timeToNewBuyer;
    [SerializeField] private float timeToBuyerAppear;
    [SerializeField] private float stepInterval;
    [SerializeField] private float stepDuration;
    [SerializeField] private float moveDuration;
    [SerializeField] private Ease ease;

    private Buyer _buyer;
    
    private Transform _positionToMove;
    private Sequence _sequence;

    private int _stepCount;

    private bool _movingForward;

    private void Start()
    {
        _buyer = GetComponent<Buyer>();
        _buyer.OnBuyerExit += GetNewBuyer;
        
        SellManager.OnOrderComplete += Move;
        
        _stepCount = (int)(moveDuration / stepInterval);

        _movingForward = true;

        GetNewBuyer();
    }

    private void OnDestroy()
    {
        _buyer.OnBuyerExit -= GetNewBuyer;
        SellManager.OnOrderComplete -= Move;
    }

    private void Move()
    {
        if (_movingForward)
        {
            _positionToMove = orderPoint;
            _movingForward = false;
        }
        else
        {
            _positionToMove = exitPoint;
            Flip();
        }

        _sequence.Append(buyerWalking.transform.DOLocalMoveY(buyerWalking.transform.position.y, stepDuration)
                .SetLoops(_stepCount, LoopType.Yoyo).SetEase(ease))
                .Join(transform.DOMove(_positionToMove.position, moveDuration)
                .SetEase(ease));
    }

    [ContextMenu("Flip")]
    private void Flip()
    {
        var localScale = buyerWalking.transform.localScale;
        localScale.x *= -1f;
        buyerWalking.transform.localScale = localScale;
    }

    private IEnumerator SetNewBuyer()
    {
        buyerWalking.SetActive(false);
        yield return new WaitForSeconds(timeToNewBuyer);
        buyerWalking.SetActive(true);

        transform.position = entrancePoint.position;

        yield return new WaitForSeconds(timeToBuyerAppear);

        Move();
    }

    private void GetNewBuyer()
    {
        if (!_movingForward)
        {
            Flip();
            _movingForward = true;
        }

        StartCoroutine(SetNewBuyer());
    }
}