using Core;
using Core.Logger;
using UnityEngine;
using UnityEngine.Events;

namespace Member.PYH._Scripts.Currency
{
    public class CurrencyManager : MonoSingleton<CurrencyManager>
    {
        public UnityEvent<int> onRepaymentEvent;
        
        public int CurrentCurrency { get; private set; } = 0;

        public bool CanUseCurrency(int price)
        {
            return CurrentCurrency - price >= 0;
        }
        public void TryUseCurrency(int price)
        {
            if (CurrentCurrency - price < 0) { Logging.Log("FAILED TO USE CURRENCY"); return; }

            UseCurrency(price);
        } // Only Using In Shop
        public void TryRepayment(int price)
        {
            if (CurrentCurrency - price < 0) { Logging.Log("FAILED TO REPAYMENT CURRENCY"); return; }
            
            UseCurrency(price);
            onRepaymentEvent?.Invoke(price);
        } // Only Using In Debt
        
        private void UseCurrency(int price)
        {
            Logging.Log("USE CURRENCY");
            CurrentCurrency = Mathf.Clamp(CurrentCurrency - price, 0, int.MaxValue);
        }
    }
}
