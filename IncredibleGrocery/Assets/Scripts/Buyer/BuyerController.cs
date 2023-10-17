using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BuyerController : MonoBehaviour
{
    [Header("Objects to move")]
    [SerializeField] private GameObject _buyerWalking;
    [SerializeField] private GameObject _buyerMover;

    [Header("Path points")]
    [SerializeField] private Transform _entrancePoint;
    [SerializeField] private Transform _orderPoint;
    [SerializeField] private Transform _exitPoint;

    [Header("Moving settings")]
    [SerializeField] private float _timeToNewBuyer;
    [SerializeField] private float _timeToBuyerAppear;
    [SerializeField] private float _stepInterval;
    [SerializeField] private float _stepDuration;
    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _ease;

    private Sequence _sequence;

    private int _stepCount;

    private bool _movingForward;

    private void Start()
    {
        _stepCount = (int)(_moveDuration / _stepInterval);

        _movingForward = true;

        SetNewBuyer();

        EventBus.OnOrderComplete += MoveToExit;
        EventBus.OnBuyerExit += SetNewBuyer;
    }

    private void OnDisable()
    {
        EventBus.OnOrderComplete -= MoveToExit;
        EventBus.OnBuyerExit -= SetNewBuyer;
    }

    private void MoveToExit()
    {
        Move(_movingForward);
    }

    private void Move(bool movingForward)
    {
        Transform positionToMove;

        if (movingForward)
        {
            positionToMove = _orderPoint;
            _movingForward = false;
        }
        else
        {
            positionToMove = _exitPoint;
            Flip();
        }

        _sequence = DOTween.Sequence();

        _sequence.Append(_buyerWalking.transform.DOLocalMoveY(_buyerWalking.transform.position.y, _stepDuration)
            .SetLoops(_stepCount, LoopType.Yoyo).SetEase(_ease))
            .Join(_buyerMover.transform.DOMove(positionToMove.position, _moveDuration)
            .SetEase(_ease));

        _sequence.Play().OnComplete(SequenceKill);
    }

    [ContextMenu("Flip")]
    private void Flip()
    {
        Vector3 localscale = _buyerWalking.transform.localScale;
        localscale.x *= -1f;
        _buyerWalking.transform.localScale = localscale;
    }

    private IEnumerator GetNewBuyer()
    {
        SetInactive();     
        yield return new WaitForSeconds(_timeToNewBuyer);
        SetActive();

        SetPosition();

        yield return new WaitForSeconds(_timeToBuyerAppear);

        Move(_movingForward);
    }

    private void SetInactive()
    {
        _buyerWalking.SetActive(false);
    }

    private void SetActive()
    {
        _buyerWalking.SetActive(true);
    }

    private void SetPosition()
    {
        _buyerMover.transform.position = _entrancePoint.position;
    }

    private void SequenceKill()
    {
        _sequence.Kill();
    }

    public void SetNewBuyer()
    {
        if (!_movingForward)
        {
            Flip();
            _movingForward = true;
        }

        StartCoroutine(GetNewBuyer());
    }
}