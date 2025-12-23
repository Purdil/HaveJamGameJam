using DG.Tweening;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TextMeshProUGUI))]
public class ApplyInfoUI : MonoBehaviour
{
    [SerializeField]private float delay;
    [SerializeField] private float punchScale;
    [SerializeField] private float punchDuration;

    private Sequence _seq;
    private TextMeshProUGUI _tMP;
    private void Awake()
    {
        _tMP = GetComponent<TextMeshProUGUI>();
        _tMP.color = Color.yellow;
    }
    public void TweenStart(string applyValue)
    {
        if (_tMP.text != "") applyValue = "\n"+applyValue;

        _seq.Append(
            DOTween.Sequence()
            .AppendCallback(() => _tMP.text += applyValue)
            .Append(transform.DOPunchPosition(Vector2.one * punchScale,punchDuration))
            .AppendInterval(delay));
    }
    public void TweenClear()
    {
        _seq.Complete();
        _tMP.text = "";
    }
}
