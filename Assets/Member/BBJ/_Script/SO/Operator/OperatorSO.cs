using Unity.VisualScripting;
using UnityEngine;
public abstract class OperatorSO : ScriptableObject, IOperator<float>
{
    [field: SerializeField]
    public string std { get; private set; }
    public abstract float Operation(float p1, float p2);
}



