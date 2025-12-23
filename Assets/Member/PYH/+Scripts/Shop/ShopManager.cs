using Core.Logger;
using Member.PYH._Scripts.Currency;
using Member.PYH._Scripts.Inventory;
using Member.PYH._Scripts.SO;
using UnityEngine;

namespace Member.PYH._Scripts.Shop
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private ItemSOList list;

        public void TryBuyItem(int price, int index)
        {
            if (!CurrencyManager.Instance.CanUseCurrency(CurrencyType.Gold, price)) { Logging.Log("Returned From Check CanUseCurrency"); return; }
            if (!InventoryManager.Instance.CanGetItem()) return;
            
            CurrencyManager.Instance.TryUseCurrency(CurrencyType.Gold, price);
            InventoryManager.Instance.TryAddItem(list.GetItem(index));
        }
    }
}
