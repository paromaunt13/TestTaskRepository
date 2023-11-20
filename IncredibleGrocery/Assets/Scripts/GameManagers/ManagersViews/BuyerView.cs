using UnityEngine;
using UnityEngine.UI;

public class BuyerView : ViewManager
{
    [SerializeField] private TimerBar timerBar;
    [SerializeField] private GameObject buyerCloud;
    [SerializeField] private Transform parentContent;
    [SerializeField] private GameObject buyerReactionPrefab;

    [Header("View settings")]
    [SerializeField] private float minWaitingTime;
    [SerializeField] private float maxWaitingTime;
    [SerializeField] private Sprite satisfiedIcon;
    [SerializeField] private Sprite unsatisfiedIcon;
    [SerializeField] private float timeToCloudDisappear;

    private Image _stateImage;
    private Buyer _buyer;
    
    private bool _isSatisfied;
    private bool _isWaiting;
    
    private float _waitingTIme;
    
    public GameObject BuyerCloud => buyerCloud;
    public Transform ParentContent => parentContent;
    public bool IsWaitingForOrderCheck { get; set; }
    public bool IsLeaveUnsatisfied { get; set; }
    
    private void Start()
    {
        _buyer = GetComponent<Buyer>();
        _buyer.OnPointReached += StartWaiting;
        _stateImage = buyerReactionPrefab.GetComponent<Image>();
        timerBar.OnWaitingTimeEnds += LeaveUnsatisfied;
    }

    private void OnDestroy()
    {
        timerBar.OnWaitingTimeEnds -= LeaveUnsatisfied;
        StopAllCoroutines();
    }

    public void SetReaction(bool isSatisfied)
    {
        StopAllCoroutines();
        
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
        SetReaction(_isSatisfied);
        AudioManager.Instance.PlaySound(SoundType.UnsatisfiedSound);
    }

    private void StartWaiting()
    {
        if (_isWaiting) return;
        _waitingTIme = Random.Range(minWaitingTime, maxWaitingTime);
        timerBar.StartTimer(_waitingTIme);
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