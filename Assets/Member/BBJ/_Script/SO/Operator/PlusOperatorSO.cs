using UnityEngine;
[CreateAssetMenu(fileName = "OperatorSO", menuName = "SO/Operator/Data/Plus")]
public class PlusOperatorSO : OperatorSO
{
    public override float Operation(float p1, float p2) => p1 + p2;
}