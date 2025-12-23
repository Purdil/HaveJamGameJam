using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class RouletteSlotUI : MonoBehaviour
{
    public TextMeshProUGUI tempTMP, slot1TMP, slot2TMP;
    public RectTransform rectMask;
    private Tween _tween;
    [SerializeField] private float duration;

    [SerializeField] private string[] strings;

    [SerializeField] private float delay;
    [SerializeField] public string _currentstring;
    public void TweenStart(string newText)
    {
        _tween?.Kill();

        var temp = rectMask.rect.height;

        // temp = 1

        // 1 = 2
        // 2 = 1 + temp
        _tween = DOTween.Sequence()
            .OnStart(() =>
            {
                tempTMP.text = slot1TMP.text;
                slot1TMP.text = slot2TMP.text;
                slot2TMP.text = newText;
            })

             .Prepend(tempTMP.transform.DOLocalMoveY(-temp, duration)
             .From(slot1TMP.transform.localPosition))

             .Join(slot1TMP.transform.DOLocalMoveY(-temp, duration)
             .From(slot2TMP.transform.localPosition))

             .Join(slot2TMP.transform.DOLocalMoveY(0, duration)
             .From(slot1TMP.transform.localPosition + new Vector3(0, temp, 0)));
    }


    internal void StartRoulette(float roulettTime, string v)
    {
        StartCoroutine(RouletteCoroutine(roulettTime, delay, v));
        _currentstring = v;
    }
    private IEnumerator RouletteCoroutine(float time, float delay, string endValue)
    {
        float timer = 0;
        Random r = new Random((int)Time.time);
        while (true)
        {
            if (timer >= time) break;

            TweenStart(strings[r.Next(0, strings.Length)]);
            yield return new WaitForSeconds(delay);
            timer += delay;
        }
        TweenStart(endValue);
        yield break;
    }
    public void ApplyItem(string text)
    {
        if (text == _currentstring) return;
        TweenStart(text);
        _currentstring = text;
    }
}
