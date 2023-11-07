using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyerStateView : ViewManager
{
    [SerializeField] private GameObject reactionCloud;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject buyerReactionPrefab;

    [Header("View settings")]
    [SerializeField] private Sprite satisfiedIcon;
    [SerializeField] private Sprite unsatisfiedIcon;
    [SerializeField] private SoundsData soundsData;
    [SerializeField] private float timeToCloudDisappear;

    private Image _stateImage;

    private bool _isSatisfied;

    private void Start()
    {
        _stateImage = buyerReactionPrefab.GetComponent<Image>();
        SellManager.OnOrderComplete += SetReaction;
    }

    private void OnDestroy()
    {
        SellManager.OnOrderComplete -= SetReaction;
    }

    private void SetReaction()
    {
        var correctOrder = SellManager.CorrectOrder;
        _isSatisfied = correctOrder;
 
        UpdateStateView(_isSatisfied);

        ShowReactionCloud();
    }

    private void UpdateStateView(bool isSatisfied)
    {
        _stateImage.sprite = isSatisfied switch
        {
            true => satisfiedIcon,
            false => unsatisfiedIcon
        };

        Instantiate(buyerReactionPrefab, contentParent);
    }

    private void ShowReactionCloud()
    {
        SetCloudView(reactionCloud.gameObject, timeToCloudDisappear,
            soundsData.BubbleAppearSound, soundsData.BubbleDisappearSound);
    }

    [ContextMenu("Flip")]
    private void FlipCloud()
    {
        var localScale = reactionCloud.transform.localScale;
        localScale.x *= -1f;
        reactionCloud.transform.localScale = localScale;
    }
}