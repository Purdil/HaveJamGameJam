using Core;
using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.Currency
{
    public class CurrencyManager : MonoSingleton<CurrencyManager>
    {
        public int CurrentCurrency { get; private set; } = 0;

        public bool CanUseCurrency(int price)
        {
            return CurrentCurrency - price >= 0;
        }
        public void TryUseCurrency(int price) // Only Using In Shop
        {
            if (CurrentCurrency - price < 0) { Logging.Log("FAILED TO USE CURRENCY"); return; }

            UseCurrency(price);
        }
    
        private void UseCurrency(int price)
        {
            Logging.Log("USE CURRENCY");
            CurrentCurrency = Mathf.Clamp(CurrentCurrency - price, 0, int.MaxValue);
        }
    }
}
