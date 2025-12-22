using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private float moveX = 100f;
    public List<Transform> btn;
    private Vector2 startPos;
    private void Awake()
    {
        startPos = transform.position;
    }
    private void Start()
    {
        Sequence seq = DOTween.Sequence();

        //seq.AppendInterval(3f);

        foreach (var i in btn)
        {
            seq.Append(i.DOMoveX(startPos.x - moveX, 0.8f).SetEase(ease));
        }
    }
}
