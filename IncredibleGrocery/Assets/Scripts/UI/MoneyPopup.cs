using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPopup : MonoBehaviour
{
    [field: SerializeField] public TMP_Text MoneyText { get; set; }
    [SerializeField] private float moveDuration;
    [SerializeField] private float moveDelay;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeDelay;
    [SerializeField] private Ease ease;
    
    private Image _popupImage;
    private Sequence _sequence;
    private Color _transparency;
    public Transform EndPosition { get; set; }
    public Transform StartPosition { get; set; }

    private void Start()
    {
        _popupImage = GetComponent<Image>();
        _transparency = _popupImage.color;
        _transparency.a = 0f;
        _popupImage.color = _transparency;
        ShowPopup();
    }

    private void OnDestroy()
    {
        transform.DOKill();
        _popupImage.DOKill();
    }

    private void ShowPopup()
    {
        _sequence.Append(_popupImage.DOFade(1f, fadeDuration)).
            Join(transform.DOMove(EndPosition.position, moveDuration)
            .SetEase(ease)
            .OnComplete(() =>
            {
                _sequence.Append(_popupImage.DOFade(0f, fadeDuration).SetDelay(fadeDelay)).
                    Join(transform.DOMove(StartPosition.position, moveDuration).SetDelay(moveDelay)
                    .SetEase(ease).OnComplete(() => Destroy(gameObject)));
            }));
    }
}
