using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StartBtn : MonoBehaviour
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

        seq.AppendInterval(3f);

        foreach (var i in btn)
        {
            seq.Append(i.DOMoveX(startPos.x - moveX, 0.5f).SetEase(ease));
        }
    }
}
