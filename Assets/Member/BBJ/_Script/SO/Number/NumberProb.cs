using System;
using UnityEngine;
[Serializable]
public class NumberProb : IProbData
{
    public int Prob { get => prob.Value; set => prob.Value = value; }

    public Action<int, int> ProbChanged { get => prob.OnValueChanged; set => prob.OnValueChanged = value; }

    [SerializeField]
    private NotifyValue<int> prob;
}