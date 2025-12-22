using UnityEngine;

[CreateAssetMenu(fileName = "OperatorSO", menuName = "SO/Operator/Data/Minus")]
public class MinusOperatorSO : OperatorSO
{
    public override float Operation(float p1, float p2) => p1 - p1;
}
