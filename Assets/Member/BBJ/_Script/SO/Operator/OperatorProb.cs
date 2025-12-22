using System;
using Unity.Collections;
using UnityEngine;
[Serializable]
public class OperatorProb : IProbData
{
    [SerializeField]
    private NotifyValue<int> prob;

    [SerializeField]
    public OperatorSO OperatorSO;

    public int Prob { get => prob.Value; set => prob.Value = value; }
    public Action<int, int> ProbChanged { get => prob.OnValueChanged; set => prob.OnValueChanged = value; }   
}