using System;
using System.Collections.Generic;
using Member.PYH._Scripts.SO;
using Member.PYH._Scripts.Ui.Shop;
using UnityEngine;
using UnityEngine.UI;

public class ShopUi : MonoBehaviour
{
    [SerializeField] private Transform itemSlotPoint;
    
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private ItemSOList list;

    [SerializeField] private List<ItemSlot> slotList = new(35);
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollTweenDuration = 0.15f;
    
    private int currentIndex;
    private Tween _scrollTween;
    public ItemSlot currentSlot { get; private set; }
    
    private void Awake()
    {
        foreach (var var in list.Items)
        {
            ItemSlot slot = Instantiate(slotPrefab, itemSlotPoint).GetComponent<ItemSlot>();
            slot.SetSlotUiSetting(var);
        }
    }

    private void Update()
    {
        
    }

    private void MoveSelection(int dir)
    {
        int next = Mathf.Clamp(currentIndex + dir, 0, slotList.Count - 1);

        while (next >= 0 && next < slotList.Count && slotList[next] == null)
        {
            next += dir;
            if (next < 0) next = 0;
            if (next >= slotList.Count) next = slotList.Count - 1;
            if (next == currentIndex) break;
        }

        if (slotList[next] == null) return;

        currentIndex = next;
        currentSlot = slotList[currentIndex];

        UpdateUi();
        CenterCurrentSlotInScroll(false);
    }
    private void UpdateUi()
    {
        if (slotList == null || slotList.Count == 0) return;

        for (int i = 0; i < slotList.Count; i++)
        {
            var slot = slotList[i];
            if (slot == null) continue;

            Image img = slot.highlight;

            if (img == null)
            {
                var hl = slot.transform.Find("SlotHighLight");
                if (hl != null) img = hl.GetComponent<Image>();
            }

            if (img == null) continue;

            img.color = (slot == currentSlot) ? Color.yellow : Color.white;
        }
    }
    private void CenterCurrentSlotInScroll(bool instant)
    {
        if (scrollRect == null) return;
        var content = scrollRect.content;
        if (content == null) return;
        if (currentSlot == null) return;

        int first = GetFirstValidIndex();
        int last = GetLastValidIndex();

        if (first >= 0 && currentIndex == first)
        {
            ScrollToNormalized(1f, instant);
            return;
        }

        if (last >= 0 && currentIndex == last)
        {
            ScrollToNormalized(0f, instant);
            return;
        }

        RectTransform viewport = scrollRect.viewport != null ? scrollRect.viewport : (RectTransform)scrollRect.transform;
        RectTransform slotRt = currentSlot.rect != null ? currentSlot.rect : currentSlot.GetComponent<RectTransform>();
        if (slotRt == null) return;

        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        Canvas.ForceUpdateCanvases();

        Bounds viewBounds = new Bounds(viewport.rect.center, viewport.rect.size);
        Bounds slotBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(viewport, slotRt);

        float deltaY = slotBounds.center.y - viewBounds.center.y;

        Vector2 original = content.anchoredPosition;
        Vector2 target = original - new Vector2(0f, deltaY);

        content.anchoredPosition = target;
        Canvas.ForceUpdateCanvases();

        Bounds contentBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(viewport, content);

        if (contentBounds.min.y > viewBounds.min.y)
            target.y += viewBounds.min.y - contentBounds.min.y;
        else if (contentBounds.max.y < viewBounds.max.y)
            target.y += viewBounds.max.y - contentBounds.max.y;

        content.anchoredPosition = original;

        _scrollTween?.Kill();
        scrollRect.StopMovement();
        scrollRect.velocity = Vector2.zero;

        if (instant || scrollTweenDuration <= 0f)
        {
            content.anchoredPosition = target;
            Canvas.ForceUpdateCanvases();
            return;
        }

        _scrollTween = content.DOAnchorPos(target, scrollTweenDuration).SetEase(Ease.OutSine);
    }
    private void ScrollToNormalized(float value, bool instant)
    {
        if (scrollRect == null) return;

        _scrollTween?.Kill();
        scrollRect.StopMovement();
        scrollRect.velocity = Vector2.zero;

        if (instant || scrollTweenDuration <= 0f)
        {
            scrollRect.verticalNormalizedPosition = value;
            Canvas.ForceUpdateCanvases();
            return;
        }

        _scrollTween = DOTween.To(
            () => scrollRect.verticalNormalizedPosition,
            v => scrollRect.verticalNormalizedPosition = v,
            value,
            scrollTweenDuration
        ).SetEase(Ease.OutSine);
    }
    private int GetFirstValidIndex()
    {
        if (slotList == null || slotList.Count == 0) return -1;

        for (int i = 0; i < slotList.Count; i++)
            if (slotList[i] != null) return i;

        return -1;
    }
    private int GetLastValidIndex()
    {
        if (slotList == null || slotList.Count == 0) return -1;

        for (int i = slotList.Count - 1; i >= 0; i--)
            if (slotList[i] != null) return i;

        return -1;
    }
}
