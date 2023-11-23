using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyerView : ViewManager
{
    [SerializeField] private TimerBar timerBar;
    [SerializeField] private GameObject buyerCloud;
    [SerializeField] private Transform parentContent;
    [SerializeField] private GameObject buyerReactionPrefab;

    [Header("View settings")]
    [SerializeField] private Sprite satisfiedIcon;
    [SerializeField] private Sprite unsatisfiedIcon;
    [SerializeField] private float timeToCloudDisappear;

    private Image _stateImage;
    private Buyer _buyer;
    
    private bool _isSatisfied;
    private bool _isWaiting;
    
    public GameObject BuyerCloud => buyerCloud;
    public Transform ParentContent => parentContent;
    public bool IsWaitingForOrderCheck { get; set; }
    public bool IsLeaveUnsatisfied { get; set; }
    public Action OnBuyerLeaved;
    
    private void Start()
    {
        _buyer = GetComponent<Buyer>();
        _stateImage = buyerReactionPrefab.GetComponent<Image>();
        _buyer.OnPointReached += StartWaiting;
        timerBar.OnWaitingTimeEnds += LeaveUnsatisfied;
    }

    private void OnDestroy()
    {
        timerBar.OnWaitingTimeEnds -= LeaveUnsatisfied;
        _buyer.OnPointReached -= StartWaiting;
        StopAllCoroutines();
    }

    public void SetReaction(bool isSatisfied)
    {
        timerBar.gameObject.SetActive(false);
        _stateImage.sprite = isSatisfied ? satisfiedIcon : unsatisfiedIcon;

        ClearReactionView();
        Instantiate(buyerReactionPrefab, ParentContent);
        SetCloudView(BuyerCloud.gameObject, timeToCloudDisappear);

        _buyer.LeaveStore();
    }

    private void ClearReactionView()
    {
        foreach (Transform child in parentContent)
            Destroy(child.gameObject);
    }

    private void LeaveUnsatisfied()
    {
        if (IsWaitingForOrderCheck) return;
        IsLeaveUnsatisfied = true;
        _isSatisfied = !IsLeaveUnsatisfied;
        OnBuyerLeaved?.Invoke();
        SetReaction(_isSatisfied);
        AudioManager.Instance.PlaySound(SoundType.UnsatisfiedSound);
    }

    private void StartWaiting(float waitingTime)
    {
        if (_isWaiting) return;
        timerBar.StartTimer(waitingTime);
        _isWaiting = true;
    }

    [ContextMenu("Flip")]
    private void FlipCloud()
    {
        var localScale = BuyerCloud.transform.localScale;
        localScale.x *= -1f;
        BuyerCloud.transform.localScale = localScale;
    }
}