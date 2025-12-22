using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "NumberProbs", menuName = "SO/Prob/Number/List")]
public class NumberProbListSO : ScriptableObject
{
    [SerializeField]
    private SerializableDictionary<string,NumberProb> numberProbList;

    public Action<NumberProbInfo[]> ValueChenged;

    private void OnEnable()
    {
        foreach (var item in numberProbList.Values)
        {
            item.ProbChanged += OnProbChenged;
        }
    }
    private void OnDisable()
    {
        foreach (var item in numberProbList.Values)
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

    private NumberProbInfo[] GetPrebInfo()
    {
        var sum = numberProbList.Values.Sum((x) => x.Prob);
        NumberProbInfo[] result = new NumberProbInfo[numberProbList.Count];

        foreach (var item in numberProbList)
        {
            int index = int.Parse(item.Key);
            result[index] = new NumberProbInfo(index, (float)item.Value.Prob / sum);
        }
        return result;
    }
    public int GetRendomNum()
    {
        var sum = numberProbList.Values.Sum((x) => x.Prob);
        float r = Random.value;
        int result = default;
        int s = 0;
        foreach (var item in numberProbList)
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
public struct NumberProbInfo
{
    public int num;
    public float prob;
    public NumberProbInfo(int num, float prob)
    {
        this.num = num;
        this.prob = prob;
    }
}
