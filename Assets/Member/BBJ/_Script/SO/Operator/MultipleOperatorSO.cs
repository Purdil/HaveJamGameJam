using UnityEngine;

[CreateAssetMenu(fileName = "OperatorSO", menuName = "SO/Operator/Data/Multiple")]
public class MultipleOperatorSO : OperatorSO
{
    public override float Operation(float p1, float p2) => p1 * p2;
}