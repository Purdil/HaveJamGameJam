using TMPro;
using UnityEngine;

public class DebtUi : MonoBehaviour
{
    [SerializeField] private TMP_Text maxDebtText, weekDebt, leftDayText;

    public void SetUi(int maxDebtText, int weekDebt, int leftDay)
    {
        this.maxDebtText.text = maxDebtText.ToString() + 'G';
        this.weekDebt.text = weekDebt.ToString() + 'G';
        leftDayText.text = leftDay.ToString() + " Day";
    }
}
