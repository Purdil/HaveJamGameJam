public struct ApplyRouletNum : IRouletApply
{
    public RouletNum RouletNum;
    public OperatorSO ApplyOperator;
    public ApplyRouletNum(int? num1 = default, int? num2 = default, int? num3 = default, OperatorSO applyOperator = default)
    {
        this.RouletNum = new RouletNum(num1, num2, num3);
        this.ApplyOperator = applyOperator;
    }
    public ApplyRouletNum(RouletNum rouletNum, OperatorSO applyOperator = default)
    {
        this.RouletNum = rouletNum;
        this.ApplyOperator = applyOperator;
    }
    public int CompareTo(object obj)
    {
        return this.ApplyOperator.Priority.CompareTo(((ApplyFinal)obj).ApplyOperator.Priority);
    }
}