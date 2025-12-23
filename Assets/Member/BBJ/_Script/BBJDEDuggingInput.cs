using UnityEngine;

public class BBJDEDuggingInput : MonoBehaviour
{
#if UNITY_EDITOR
    public NumberProbListSO number;
    public OperatorProbListSO oper;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            RouletteManager.Instance.StartPlayerSpin(number,oper);
        }
    }
#endif
}
