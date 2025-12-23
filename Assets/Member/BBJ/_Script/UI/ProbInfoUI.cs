using System;
using System.Text;
using TMPro;
using UnityEngine;

public class ProbInfoUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI numTMP;
    [SerializeField]private TextMeshProUGUI operTMP;

    [SerializeField]private NumberProbListSO numData;
    [SerializeField]private OperatorProbListSO operData;

    private void Awake()
    {
        numData.ValueChenged += ApplyNumProb;
        operData.ValueChenged += ApplyOperProb;
    }

    private void ApplyNumProb(NumberProbInfo[] info)
    {
        var sb = new StringBuilder();
        foreach(var item in info)
        {
            sb.Append(item.num);
            sb.Append(" : ");
            sb.Append((item.prob * 100).ToString("F2"));
            sb.Append("%\n");
        }
        numTMP.text = sb.ToString();
    }

    private void ApplyOperProb(OperatorProbInfo[] info)
    {
        var sb = new StringBuilder();
        foreach (var item in info)
        {
            sb.Append(item.str);
            sb.Append(" : ");
            sb.Append((item.prob*100).ToString("F2"));
            sb.Append("%\n");
        }
        operTMP.text = sb.ToString();
    }

    private void OnDestroy()
    {
        numData.ValueChenged -= ApplyNumProb;
        operData.ValueChenged -= ApplyOperProb;
    }
}
