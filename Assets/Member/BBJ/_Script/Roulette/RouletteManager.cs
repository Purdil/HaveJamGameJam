using System;
using UnityEngine;

public class RouletteManager : MonoBehaviour
{
    [SerializeField] private NumberProbListSO numProb;
    [SerializeField] private OperatorProbListSO operProb;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
    }
#endif
}

public struct RouletNum: IRouletApply, IRouletInfo
{
    public int Num1, Num2, Num3;
    public int Priority;
}
public struct ApplyRouletNum: IRouletApply
{
    public RouletNum ApplyNum;
    public OperatorSO ApplyOperator;
}
public struct RouletOperator:IRouletApply, IRouletInfo
{
    public OperatorSO Operator1, OperatorSO2;
}
public interface IRouletApply{}
public interface IRouletInfo {}
