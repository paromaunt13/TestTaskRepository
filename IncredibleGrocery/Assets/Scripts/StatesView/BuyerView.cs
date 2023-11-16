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
    private bool _isLeaveUnsatisfied;
    private bool _isWaiting;

    private float _waitingTIme;
    public GameObject BuyerCloud => buyerCloud;

    public Transform ParentContent => parentContent;

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
    }

    public void SetReaction(bool isSatisfied)
    {
        timerBar.gameObject.SetActive(false);
        //if (_isLeaveUnsatisfied) return;
        _stateImage.sprite = isSatisfied ? satisfiedIcon : unsatisfiedIcon;

        ClearReactionView();
        Instantiate(buyerReactionPrefab, ParentContent);
        SetCloudView(BuyerCloud.gameObject, timeToCloudDisappear);
        
        _buyer.LeaveStore();
    }

    private void ClearReactionView()
    {
        foreach (Transform child in parentContent)
        {
            Destroy(child.gameObject);
        }
    }

    private void LeaveUnsatisfied()
    {
        _isLeaveUnsatisfied = true;
        _isSatisfied = false;
        SetReaction(_isSatisfied);
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