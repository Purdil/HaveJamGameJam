using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

[CreateAssetMenu(fileName = "OperatorProbs", menuName = "SO/Prob/Operator/List")]
public class OperatorProbListSO : ScriptableObject
{
    [SerializeField]
    private SerializableDictionary<string,OperatorProb> OperatorberProbList = new();

    public Action<OperatorProbInfo[]> ValueChenged;


    private void OnEnable()
    {
        foreach (var item in OperatorberProbList.Values)
        {
            item.ProbChanged += OnProbChenged;
        }
    }
    private void OnDisable()
    {
        foreach (var item in OperatorberProbList.Values)
        {
            item.ProbChanged -= OnProbChenged;
        }
    }
    public void OnProbChenged(int a, int b) => OnProbChenged();
    public void OnProbChenged()
    {
        var probs = GetPrebInfo();
        ValueChenged?.Invoke(probs);
    }

    private OperatorProbInfo[] GetPrebInfo()
    {
        var sum = OperatorberProbList.Values.Sum((x) => x.Prob);
        OperatorProbInfo[] result = new OperatorProbInfo[OperatorberProbList.Count];
        int i = 0;
        foreach (var item in OperatorberProbList)
        {
            result[i] = new OperatorProbInfo(item.Key, (float)item.Value.Prob / sum);
            i++;
        }
        return result;
    }
    public int GetRendomNum()
    {
        var sum = OperatorberProbList.Values.Sum((x) => x.Prob);
        float r = Random.value;
        int result = default;
        int s = 0;
        foreach (var item in OperatorberProbList)
        {
            s += item.Value.Prob;
            if (s > r)
            {
                result = int.Parse(item.Key);
                break;
            }
        }
        return result;
    }
}
public struct OperatorProbInfo
{
    public string str;
    public float prob;
    public OperatorProbInfo(string num, float prob)
    {
        this.str = num;
        this.prob = prob;
    }
}

