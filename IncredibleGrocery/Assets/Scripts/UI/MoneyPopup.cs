using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    private const float MoveDuration = 1f;
    private const float MoveDelay = 0.2f;
    private const float FadeDuration = 1f;
    private const float FadeDelay = 0.5f;
    private const Ease Ease = DG.Tweening.Ease.InOutQuart;
    
    private Image _popupImage;
    private Sequence _sequence;
    private Color _transparency;

    private void OnEnable()
    {
        _popupImage = GetComponent<Image>();
        _transparency = _popupImage.color;
        _transparency.a = 0f;
        _popupImage.color = _transparency;
    }

    private void OnDestroy()
    {
        transform.DOKill();
        _popupImage.DOKill();
    }

    public void ShowPopup(Transform startPoint, Transform endPoint, string moneyPopupText)
    {
        moneyText.text = moneyPopupText;
        
        _sequence.Append(_popupImage.DOFade(1f, FadeDuration)).
            Join(transform.DOMove(endPoint.position, MoveDuration)
            .SetEase(Ease)
            .OnComplete(() =>
            {
                _sequence.Append(_popupImage.DOFade(0f, FadeDuration).SetDelay(FadeDelay)).
                    Join(transform.DOMove(startPoint.position, MoveDuration).SetDelay(MoveDelay)
                    .SetEase(Ease).OnComplete(() => Destroy(gameObject)));
            }));
    }
}
