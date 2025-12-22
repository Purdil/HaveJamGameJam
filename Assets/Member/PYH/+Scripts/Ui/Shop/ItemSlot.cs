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
        public ItemSO Item { get; private set; }
    
        [SerializeField] public Image highlight;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemDesc;
        public int ItemIndex { get; private set; }
        
        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        public void SetSlotUiSetting(ItemSO item)
        {
            Item = item;
            itemName.text = Item.ItemName;
            itemDesc.text = Item.Description;
            itemIcon.sprite = Item.Icon;
            ItemIndex = item.index;
        }
    }
}
