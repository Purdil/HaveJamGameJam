using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(TextMeshProUGUI))]
public class ApplyInfoUI : MonoBehaviour
{
    [SerializeField]private float delay;
    [SerializeField] private float punchScale;
    [SerializeField] private float punchDuration;
    public List<ApplyStruct> list = new(); 

    private Sequence _seq;
    private TextMeshProUGUI _tMP;
    private void Awake()
    {
        _tMP = GetComponent<TextMeshProUGUI>();
        _tMP.color = Color.yellow;
        _tMP.text = "";
    }
    public void TweenStart(ApplyStruct[] value)
    {
        for(int i = list.Count; i < value.Length; i++)
        {
            list.Add(value[i]);
            string a;
            if (_tMP.text != "") a = "\n" + value[i].str;
            else a = value[i].str;

            _seq.Append(
                DOTween.Sequence()
                .AppendCallback(() => _tMP.text += a)
                .Append(transform.DOPunchPosition(Vector2.one * punchScale, punchDuration))
                .AppendInterval(delay));
        }
    }
    public void TweenClear()
    {
        _seq.Complete();
        _tMP.text = "";
    }
}
