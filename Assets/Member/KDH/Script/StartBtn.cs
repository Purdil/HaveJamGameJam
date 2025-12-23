using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StartBtn : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private float moveX = 10f;
    public List<Transform> btn;
    private Vector2 startPos;

    [SerializeField]private float moveTime = 0.2f;

    private void Awake()
    {
        startPos = transform.position;
    }
    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        foreach (var i in btn)
        {
            seq.Append(i.DOMoveX(startPos.x - moveX, moveTime).SetEase(ease));

        }
    }
}
