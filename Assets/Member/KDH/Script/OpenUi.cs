using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.AppUI.UI;
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
    [SerializeField] private GameObject panel;

    private int count = 0;
    private bool isOpen;

    private void Awake()
    {
        SetActive(isOpen);
        startPos = transform.position;
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SettingToggle();
        }
    }

    public void OnCountinue()
    {
        isOpen = false;
        SetActive(isOpen);
        Time.timeScale = 1f;
    }
    private void SettingToggle()
    {
        isOpen = !isOpen;
        SetActive(isOpen);

        Time.timeScale = isOpen ? 0f : 1f;
    }

    public void SetActive(bool isOpen)
    {
        if (count > 2) return;  
        gameUi.SetActive(isOpen);

        if (isOpen)
        {
            a?.Complete();
            Sequence seq = DOTween.Sequence().SetUpdate(true);
            foreach (var i in btn)
            {
                seq.Append(i.DOMoveX(moveX, 0.1f).From().SetEase(ease));
            }
            a = seq;
            count++;
        }
        else
            count = 0;
    }
}
