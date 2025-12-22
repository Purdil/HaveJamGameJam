using Core.Logger;
using Member.PYH._Scripts.Abstract;
using UnityEngine;

namespace Member.PYH._Scripts.SO
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ITEM/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public int index;
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public string ItemName { get; private set; } = "Please Enter Item Name Here..."; // 아이템 이름
        [field: SerializeField] public string Description { get; private set; } = "Please Enter Item Description Here..."; // 아이템 설명

        [field: SerializeField] public int Durability { get; private set; } = 0; // 아이템 내구도
        [field: SerializeField] public float ActiveProbability { get; private set; } = 0; // 발동 확률
        [field: SerializeField] public float ActiveTurn { get; private set; } = 0; // 발동되는 턴
        
        [field: SerializeField] public bool IsDisposable { get; private set; } // 이 아이템은 n회용인가?
        [field: SerializeField] public bool IsPassive { get; private set; } // 이 아이템은 패시브용인가?

        public ItemBase itemBase;

        private void OnValidate()
        {
            if (itemBase != null)
            {
                if (ItemName.Length == 0) Logging.LogWarning("Empty Item Name");
                if (Description.Length == 0) Logging.LogWarning("Empty Item Description");
            }
        }
    }
}