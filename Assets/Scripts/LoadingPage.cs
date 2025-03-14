using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LoadingPage : MonoBehaviour
{
    [SerializeField] private CanvasGroup studio, present, game;
    [SerializeField] private UnityEvent onComplete;

    private void Awake()
    {
        studio.alpha = 0;
        present.alpha = 0;
        game.alpha = 0;
    }

    void Start()
    {
        studio.DOFade(1, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            studio.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                present.DOFade(1, 1).SetEase(Ease.Linear).OnComplete(() =>
                {
                    present.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        game.DOFade(1, 1).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            game.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
                            {
                                onComplete.Invoke();
                            });
                        });
                    });
                });
            });
        });
    }
}