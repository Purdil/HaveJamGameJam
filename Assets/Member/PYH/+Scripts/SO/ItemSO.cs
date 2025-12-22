using Core.Logger;
using Member.PYH._Scripts.Abstract;
using UnityEngine;

namespace Member.PYH._Scripts.SO
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ITEM/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public int index;
        [field: SerializeField] public string Name { get; private set; } = "Please Enter Item Name Here...";
        [field: SerializeField] public string Description { get; private set; } = "Please Enter Item Description Here...";
        
        [field: SerializeField] public bool IsDisposable { get; private set; }
        [field: SerializeField] public bool IsPassive { get; private set; }

        public ItemBase ItemBase;

        private void OnValidate()
        {
            if (ItemBase != null)
            {
                if (Name.Length == 0) Logging.LogWarning("Empty Item Name");
                if (Description.Length == 0) Logging.LogWarning("Empty Item Description");
            }
        }
    }
}