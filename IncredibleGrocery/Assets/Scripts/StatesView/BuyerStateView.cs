using UnityEngine;
using UnityEngine.UI;

public class BuyerStateView : MonoBehaviour
{
    [SerializeField] private GameObject _reactionCloud;

    [Header("View settings")]
    [SerializeField] private Image _stateImage;

    [SerializeField] private Sprite _satisfiedImage;
    [SerializeField] private Sprite _unatisfiedImage;
    
    [SerializeField] private SoundsData _soundsData;

    [SerializeField] private float _timeToCloudDisappear;

    private BuyerState _buyerState;

    private void Start()
    {
        EventBus.OnOrderComplete += SetReaction;
    }

    private void OnDisable()
    {
        EventBus.OnOrderComplete -= SetReaction;
    }

    private void SetReaction()
    {
        bool correctOrder = SellManager.CorrectOrder;

        if (correctOrder)
        {
            _buyerState = BuyerState.Satisfied;
        }
        else
        {
            _buyerState = BuyerState.Unsatisfied;
        }

        UpdateStateView(_buyerState);

        ShowReactionCloud();
    }

    private void UpdateStateView(BuyerState buyerState)
    {
        _stateImage.gameObject.SetActive(true);

        switch (buyerState)
        {
            case BuyerState.Satisfied:
                _stateImage.sprite = _satisfiedImage;
                break;
            case BuyerState.Unsatisfied:
                _stateImage.sprite = _unatisfiedImage;
                break;
        }
    }

    private void ShowReactionCloud()
    {
        ViewManager.Instance.SetCloudView(_reactionCloud.gameObject, _timeToCloudDisappear,
            _soundsData.BubbleAppearSound, _soundsData.BubbleDisappearSound);
    }

    [ContextMenu("Flip")]
    private void FlipCloud()
    {
        Vector3 localscale = _reactionCloud.transform.localScale;
        localscale.x *= -1f;
        _reactionCloud.transform.localScale = localscale;
    }
}