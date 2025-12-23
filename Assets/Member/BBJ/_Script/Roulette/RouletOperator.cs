using System.Collections.Generic;
using Unity.XR.OpenVR;
public struct RouletOperator : IRouletApply, IRouletInfo, IPriority
{

    public Vector<OperatorSO, int>[] oper;
    public OperatorSO Operator1
    {
        get
        {
            if (oper == null) oper = new Vector<OperatorSO, int>[2];
            return oper[0].x;
        }
        set
        {
            if (oper == null) oper = new Vector<OperatorSO, int>[2];
            oper[0].x = value;
        }
    }
    public OperatorSO Operator2
    {
        get
        {
            if (oper == null) oper = new Vector<OperatorSO, int>[2];
            return oper[1].x;
        }
        set
        {
            if (oper == null) oper = new Vector<OperatorSO, int>[2];
            oper[1].x = value;
        }
    }

    public int Priority { get; set; }
    public RouletOperator(OperatorSO operator1 = default, OperatorSO operator2 = default, int priority = default)
    {
        oper = new Vector<OperatorSO, int>[2];
        oper[0] = new Vector<OperatorSO, int>(operator1, priority);
        oper[1] = new Vector<OperatorSO, int>(operator2, priority);
        this.Priority = priority;
    }
    public RouletOperator(int priority = default)
    {
        oper = new Vector<OperatorSO, int>[2];
        oper[0] = new Vector<OperatorSO, int>(null, priority);
        oper[1] = new Vector<OperatorSO, int>(null, priority);
        this.Priority = priority;
    }

    internal void Apply(RouletOperator p2)
    {
        if (p2.oper == null) return;

        if (p2.Operator1 != null&&( this.Operator1 == null || this.oper[0].y < p2.oper[0].y))
            this.oper[0] = p2.oper[0];
        if (p2.Operator2 != null&&( this.Operator2 == null || this.oper[1].y < p2.oper[1].y))
            this.oper[1] = p2.oper[1];
    }
}