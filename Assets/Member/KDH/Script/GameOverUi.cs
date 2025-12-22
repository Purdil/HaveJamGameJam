using DG.Tweening;
using UnityEngine;

public class GameOverUi : MonoBehaviour
{
    [System.Serializable]
    public struct BtnData
    {
        public RectTransform btn;
        public float height;
    }

    [SerializeField] private BtnData[] buttons;
    [SerializeField] private float time = 0.3f;

    public void Move()
    {
        Debug.Log("Move called");
        foreach (var b in buttons)
        {
            b.btn.DOAnchorPos(
                b.btn.anchoredPosition + Vector2.up * b.height,
                time
            ).SetUpdate(true);
        }
    }
}

