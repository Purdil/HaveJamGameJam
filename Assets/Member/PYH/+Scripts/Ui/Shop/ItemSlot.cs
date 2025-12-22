using System;
using Member.PYH._Scripts.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Member.PYH._Scripts.Ui.Shop
{
    public class ItemSlot : MonoBehaviour
    {
        public int index;
        public RectTransform rect;
        private ItemSO _item;
    
        [SerializeField] public Image highlight;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemName;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        public void SetSlotUiSetting(ItemSO item)
        {
            _item = item;
            itemName.text = _item.ItemName;
            itemIcon.sprite = _item.Icon;
        }
    }
}
