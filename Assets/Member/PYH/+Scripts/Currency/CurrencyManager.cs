using Core;
using UnityEngine;

public class CurrencyManager : MonoSingleton<CurrencyManager>
{
    public int CurrentCurrency { get; private set; } = 0;

    public bool CanUseCurrency(int price, int itemIndex) // Only Using In Shop
    {
        if (CurrentCurrency - price < 0) return false;

        UseCurrency(price);
        DisburseItem(itemIndex);
        return true;
    }
    
    private void UseCurrency(int price)
    {
        CurrentCurrency = Mathf.Clamp(CurrentCurrency - price, 0, int.MaxValue);
    }

    private void DisburseItem(int itemIndex)
    {
        // 여기서 인벤토리 매니저에게 아이템 지급
    }
}
