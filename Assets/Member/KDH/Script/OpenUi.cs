using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenUi : MonoBehaviour
{
    [SerializeField] private GameObject gameUi;
    [SerializeField] private Ease ease;
    [SerializeField] private float moveX = 10f;
    public List<Transform> btn;
    private Vector2 startPos;
    private Tween a;

    private bool isOpen;

    private void Awake()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SettingToggle();
        }
    }
    private void SettingToggle()
    {
        isOpen = !isOpen;
        SetActive(isOpen);

        Time.timeScale = isOpen ? 0f : 1f;
    }

    private void SetActive(bool isOpen)
    {
        gameUi.SetActive(isOpen);

        if (isOpen)
        {
            a?.Complete();
            Sequence seq = DOTween.Sequence().SetUpdate(true);
            foreach (var i in btn)
            {
                seq.Append(i.DOMoveX(moveX, 0.1f)
                    .From()
                    .SetEase(ease));
            }
            a = seq;

        }
    }
}
