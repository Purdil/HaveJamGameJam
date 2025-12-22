using Member.PYH._Scripts.SO;
using UnityEngine;

namespace Member.PYH._Scripts.Shop
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private ItemSOList list;

        public bool TryBuyItem(int price, int index)
        {
            if (!CurrencyManager.Instance.CanUseCurrency(price, index)) { return false; }
            
            return true;
        }
    }
}
