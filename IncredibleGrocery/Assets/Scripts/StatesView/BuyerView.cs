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

    private float _waitingTIme;
    public GameObject BuyerCloud => buyerCloud;

    public Transform ParentContent => parentContent;

    private void Start()
    {
        _buyer = GetComponent<Buyer>();
        _stateImage = buyerReactionPrefab.GetComponent<Image>();
        timerBar.OnTimerEnds += SetUnsatisfied;
        
        OrderView.OnOrderViewCreated += StartWaiting;
        SellManager.OnOrderComplete += SetReaction;
    }

    private void OnDestroy()
    {
        OrderView.OnOrderViewCreated -= StartWaiting;
        SellManager.OnOrderComplete -= SetReaction;
        timerBar.OnTimerEnds -= SetUnsatisfied;
    }

    private void SetReaction(bool isSatisfied)
    {
        timerBar.IsWaitingTimeEnds = false;
        timerBar.gameObject.SetActive(false);
        _stateImage.sprite = isSatisfied ? satisfiedIcon : unsatisfiedIcon;
        Instantiate(buyerReactionPrefab, ParentContent);
        SetCloudView(BuyerCloud.gameObject, timeToCloudDisappear);
    }

    private void SetUnsatisfied()
    {
        _buyer.LeaveStore();
        _isSatisfied = false;
        SetReaction(_isSatisfied);
    }


    private void StartWaiting()
    {
        _waitingTIme = Random.Range(minWaitingTime, maxWaitingTime);
        timerBar.StartTimer(_waitingTIme);
    }
    
    [ContextMenu("Flip")]
    private void FlipCloud()
    {
        var localScale = BuyerCloud.transform.localScale;
        localScale.x *= -1f;
        BuyerCloud.transform.localScale = localScale;
    }
}