using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    [SerializeField] private Ease moveEase;
    private Ease spinEase;

    [SerializeField] private float moveY = 10f;      
    [SerializeField] private float changDisY = -1.2f;

    [SerializeField] private List<Transform> btn;

    private Vector2 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Start()
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < btn.Count; i++)
        {

            seq.AppendInterval(1f);

            Transform t = btn[i];


            float targetY = startPos.y + moveY + (i * changDisY);

            seq.Append(
                t.DOMoveY(targetY, 0.5f)
                 .SetEase(moveEase)
            );

            seq.Append(t.DOScaleX(0f, 0.1f).SetEase(spinEase));
            seq.Append(t.DOScaleX(1f, 0.1f).SetEase(spinEase));
        }
    }
}
