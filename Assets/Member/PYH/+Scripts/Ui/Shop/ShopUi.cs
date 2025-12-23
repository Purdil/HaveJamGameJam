using System.Collections.Generic;
using DG.Tweening;
using Member.PYH._Scripts.SO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Member.PYH._Scripts.Ui.Shop
{
    public enum ChannelEnum
    {
        Shop,
        BlackMarket
    }
    
    public class ShopUi : MonoBehaviour
    {
        [SerializeField] private Transform itemSlotPoint;
    
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private ItemSOList list;

        [SerializeField] private List<ItemSlot> slotList = new(35);
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform content;
        [SerializeField] private float scrollTweenDuration = 0.15f;

        [SerializeField] private TMP_Text enterButtonText;
        [SerializeField] private TMP_Text shopMainMsgText, shopMiniMsgText;
        [SerializeField] private RectMask2D mask;
        
        [SerializeField] private ChannelEnum currentChannel;
        [SerializeField] private GameObject shopChannel, blackmarketChannel;
        [SerializeField] private Image channelFadeImage;

        [SerializeField] private RectTransform shopUi;
        [SerializeField] private Image background;

        private Sequence _selectSeq;
        private bool _isActive;
        private bool _fading;
        private bool _moving;
        private int _currentIndex;
        private Tween _scrollTween;
        public ItemSlot CurrentSlot { get; private set; }
        public UnityEvent<int, int> onTryBuyEvent;
        public UnityEvent onSellEvent;
    
        private void Awake()
        {
            foreach (var var in list.Items)
            {
                ItemSlot slot = Instantiate(slotPrefab, itemSlotPoint).GetComponent<ItemSlot>();
                slot.index = var.index - 1;
                slot.SetSlotUiSetting(var);
            }

            SelectChannel(ChannelEnum.Shop);
            BuildSlotList();
        }
        private void Update()
        {
            if (Keyboard.current.f1Key.wasPressedThisFrame)
                OpenUi();
            if (Keyboard.current.f2Key.wasPressedThisFrame)
                HideUi();

            if (Keyboard.current == null) return;
            if (slotList == null || slotList.Count == 0) return;
            if (!_isActive) return;
            if (_moving) return;
            if (!gameObject.activeInHierarchy) return;
            if (_fading) return;
            if (CurrentSlot == null)
            {
                _currentIndex = GetFirstValidIndex();
                if (_currentIndex < 0) return;
                CurrentSlot = slotList[_currentIndex];
                UpdateUi();
                CenterCurrentSlotInScroll(true);
            }
            
            if (Keyboard.current.upArrowKey.wasPressedThisFrame && currentChannel == ChannelEnum.Shop)
                MoveSelection(-1);

            if (Keyboard.current.downArrowKey.wasPressedThisFrame && currentChannel == ChannelEnum.Shop)
                MoveSelection(1);

            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                if (currentChannel == ChannelEnum.Shop)
                {
                    onTryBuyEvent?.Invoke(CurrentSlot.Item.ItemPrice, CurrentSlot.ItemIndex);
                }
                else if (currentChannel == ChannelEnum.BlackMarket)
                {
                    onSellEvent?.Invoke();
                }
            }
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                SelectChannel(ChannelEnum.Shop);
            }
            if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                SelectChannel(ChannelEnum.BlackMarket);
            }
        }
        private void ResetUi()
        {
            channelFadeImage.color = 
                new Color(channelFadeImage.color.r, channelFadeImage.color.g, channelFadeImage.color.b, 1);
            enterButtonText.color =
                new Color(enterButtonText.color.r, enterButtonText.color.g, enterButtonText.color.b, 0);
            shopMainMsgText.color =
                new Color(shopMainMsgText.color.r, shopMainMsgText.color.g, shopMainMsgText.color.b, 0);
            shopMiniMsgText.color =
                new Color(shopMiniMsgText.color.r, shopMiniMsgText.color.g, shopMiniMsgText.color.b, 0);
            currentChannel = ChannelEnum.BlackMarket;
            SelectChannel(ChannelEnum.Shop);
            UpdateUi();
        }

        #region  For Ui Move
        public void HideUi()
        {
            if (_fading) return;
            if (!_isActive) return;
            if (_moving) return;
            
            _moving = true;
            
            Sequence seq = DOTween.Sequence();
            seq.Append(shopUi.DOAnchorPosY(-2500, 1.3f));
            seq.Join(background.DOFade(0, 1.25f));
            seq.AppendCallback(() =>
            {
                _isActive = false;
                _moving = false;
            });
        }
        public void OpenUi()
        {
            if (_isActive) return;
            if (_moving) return;

            ResetUi();
            _isActive = true;
            _moving = true;
            
            Sequence seq = DOTween.Sequence();
            seq.Append(background.DOFade(1, 1.25f));
            seq.Join(shopUi.DOAnchorPosY(0, 1.3f));
            seq.AppendCallback(() =>
            {
                _moving = false;
            });
        }
        
        private void MoveSelection(int dir)
        {
            int next = Mathf.Clamp(_currentIndex + dir, 0, slotList.Count - 1);

            while (next >= 0 && next < slotList.Count && slotList[next] == null)
            {
                next += dir;
                if (next < 0) next = 0;
                if (next >= slotList.Count) next = slotList.Count - 1;
                if (next == _currentIndex) break;
            }

            if (slotList[next] == null) return;
            _currentIndex = next;
            CurrentSlot = slotList[_currentIndex];
            
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
                TMP_Text text = slot.itemName;
                
                if (img == null) continue;

                img.color = (slot == CurrentSlot) ? Color.yellow : Color.white;
                text.color = (slot == CurrentSlot) ? Color.yellow : Color.white;
            }
        }
        private void CenterCurrentSlotInScroll(bool instant)
        {
            if (scrollRect == null) return;
            var content = scrollRect.content;
            if (content == null) return;
            if (CurrentSlot == null) return;

            int first = GetFirstValidIndex();
            int last = GetLastValidIndex();

            if (first >= 0 && _currentIndex == first)
            {
                ScrollToNormalized(1f, instant);
                return;
            }

            if (last >= 0 && _currentIndex == last)
            {
                ScrollToNormalized(0f, instant);
                return;
            }

            RectTransform viewport = scrollRect.viewport != null ? scrollRect.viewport : (RectTransform)scrollRect.transform;
            RectTransform slotRt = CurrentSlot.rect != null ? CurrentSlot.rect : CurrentSlot.GetComponent<RectTransform>();
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
        private void BuildSlotList()
        {
            slotList.Clear();
            if (content == null) return;

            int max = -1;

            for (int i = 0; i < content.childCount; i++)
            {
                var rs = content.GetChild(i).GetComponent<ItemSlot>();
                if (rs == null) continue;
                if (rs.index < 0) continue;
                if (rs.index > max) max = rs.index;
            }

            if (max < 0) return;

            for (int i = 0; i <= max; i++) slotList.Add(null);

            for (int i = 0; i < content.childCount; i++)
            {
                var rs = content.GetChild(i).GetComponent<ItemSlot>();
                if (rs == null) continue;
                if (rs.index < 0 || rs.index >= slotList.Count) continue;

                slotList[rs.index] = rs;
            }
        }
        #endregion
        #region  For Channel Move
        private void SelectChannel(ChannelEnum channel)
        {
            if (currentChannel == channel) return;
            if (_fading) return;

            _selectSeq?.Kill();
            channelFadeImage.DOKill();
            enterButtonText.DOKill();

            _fading = true;

            string nextText;
            string nextMsg1, nextMsg2;
            bool shopActive;
            bool blackActive;

            switch (channel)
            {
                case ChannelEnum.Shop:
                    nextText = "구매하기 | (ENTER)";
                    nextMsg1 = "[[특별$한]] 상$$점!!!";
                    nextMsg2 = "지금 당장 구매하세요!!";
                    shopActive = true;
                    blackActive = false;
                    break;

                case ChannelEnum.BlackMarket:
                    nextText = "판매하기 | (ENTER)";
                    nextMsg1 = "그가 운영하는 암시장";
                    nextMsg2 = "\"언제나 환영합니다.\"";
                    shopActive = false;
                    blackActive = true;
                    break;

                default:
                    _fading = false;
                    return;
            }

            _selectSeq = DOTween.Sequence();

            _selectSeq.Append(channelFadeImage.DOFade(1f, 1f));

            _selectSeq.AppendCallback(() =>
            {
                currentChannel = channel;

                shopChannel.SetActive(shopActive);
                blackmarketChannel.SetActive(blackActive);
            });

            _selectSeq.Join(enterButtonText.DOFade(0f, 0.15f));
            _selectSeq.Join(shopMainMsgText.DOFade(0f, 0.15f));
            _selectSeq.Join(shopMiniMsgText.DOFade(0f, 0.15f));
            _selectSeq.AppendCallback(() =>
            {
                enterButtonText.text = nextText;
                shopMainMsgText.text = nextMsg1;
                shopMiniMsgText.text = nextMsg2;
            });
            _selectSeq.Append(enterButtonText.DOFade(1f, 0.15f));
            _selectSeq.Append(shopMainMsgText.DOFade(1f, 0.15f));
            _selectSeq.Append(shopMiniMsgText.DOFade(1f, 0.15f));
            _selectSeq.Join(channelFadeImage.DOFade(0f, 1f));

            _selectSeq.OnComplete(() => _fading = false);
            _selectSeq.Play();
        }
        #endregion
    }
}
