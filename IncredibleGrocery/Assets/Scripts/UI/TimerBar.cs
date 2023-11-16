using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient gradient;

    private Tween _tween;
    
    public Action OnWaitingTimeEnds;
    
    public void StartTimer(float waitingTime)
    {
        gameObject.SetActive(true);
        _tween = slider.DOValue(0f, waitingTime).SetEase((Ease.Linear)).OnComplete(() =>
         {
             if (gameObject.activeSelf && !gameObject.IsDestroyed()) 
                 OnWaitingTimeEnds?.Invoke();
             else
                 gameObject.SetActive(false);
         });
        slider.onValueChanged.AddListener(SetColor);
    }

    private void OnDisable()
    {
        _tween.Kill();
    }

    private void SetColor(float value)
    {
        fillImage.color = gradient.Evaluate(value);
    }
}
