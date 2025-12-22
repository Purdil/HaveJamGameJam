using BBJ;
using Core.Logger;
using Member.PYH._Scripts.Abstract;
using UnityEngine;

namespace Member.PYH._Scripts.SO
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ITEM/ItemSO")]
    public abstract class ItemSO : ScriptableObject
    {
        public int index;
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public string ItemName { get; private set; } = "Please Enter Item Name Here..."; // 아이템 이름
        [field: SerializeField] public string Description { get; private set; } = "Please Enter Item Description Here..."; // 아이템 설명
        [field: SerializeField] public int ItemPrice { get; private set; } = 0; // 상점 내 아이템 가격

        [field: SerializeField] public float ActiveTurn { get; private set; } = 0; // 발동되는 턴

        private void OnValidate()
        {
            if (ItemName.Length == 0) Logging.LogWarning("Empty Item Name");
            if (Description.Length == 0) Logging.LogWarning("Empty Item Description");
        }
    }
}