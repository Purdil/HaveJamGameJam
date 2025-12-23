using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUi : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private float moveX = 10f;
    public List<Transform> btn;
    private Vector2 startPos;
    private void Awake()
    {
        startPos = transform.position;
    }
    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        foreach (var i in btn)
        {
            seq.AppendInterval(0.7f);

            seq.Append(i.DOMoveX(startPos.x - moveX, 0.3f).SetEase(ease));

        }
    }
}
