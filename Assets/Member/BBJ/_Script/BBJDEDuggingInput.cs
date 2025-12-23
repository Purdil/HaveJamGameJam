using DG.Tweening;
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
            var a =RouletteManager.Instance.StartPlayerSpin(number, oper, list);
            DOVirtual.DelayedCall(1, () => Debug.Log(a()));
        }
    }
#endif
}
