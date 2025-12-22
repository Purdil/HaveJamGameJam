using UnityEngine;

[CreateAssetMenu(fileName = "OperatorSO", menuName = "SO/Operator/Data/Division")]
public class DivisionOperatorSO : OperatorSO
{
    public override float Operation(float p1, float p2)
    {
        p2 = p2 == 0 ? 0 : 1 / p2;
        return p1 * p2;
    }
}
