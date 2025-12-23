using TMPro;
using UnityEngine;

public class CurrencyUi : MonoBehaviour
{
    [SerializeField] private TMP_Text currencyText;

    public void SetText(int currency)
    {
        currencyText.text = currency.ToString("N0") + "G";
    }
}
