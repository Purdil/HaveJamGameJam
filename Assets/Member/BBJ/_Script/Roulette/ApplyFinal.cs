using System;
using UnityEditor.Experimental.GraphView;

public struct ApplyFinal : IRouletApply, IComparable
{
    public int final;
    public OperatorSO ApplyOperator;
    public ApplyFinal(int final = default, OperatorSO applyOperator = default)
    {
        this.final = final;
        this.ApplyOperator = applyOperator;
    }

    public int CompareTo(object obj)
    {
       // return this.ApplyOperator.Priority.CompareTo(((ApplyFinal)obj).ApplyOperator.Priority);
       return 0;
    }
}