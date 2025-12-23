using Member.PYH._Scripts.SO;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BBJDEDuggingInput : MonoBehaviour
{
#if UNITY_EDITOR
    public NumberProbListSO number;
    public OperatorProbListSO oper;
    public List<ItemSO> list;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log( RouletteManager.Instance.StartPlayerSpin(number,oper, list)());
        }
    }
#endif
}
