using System.Collections;
using TMPro;
using UnityEngine;

public class TextUp : MonoBehaviour
{
    private string text;
    public TMP_Text targetText;
    [SerializeField] private float delay = 0.35f;

    void Start()
    {
        text = targetText.text.ToString();
        targetText.text = " ";

        StartCoroutine(textPrint(delay));
    }

    IEnumerator textPrint(float d)
    {
        int count = 0;

        while (count != text.Length)
        {
            if (count < text.Length)
            {
                targetText.text += text[count].ToString();
                count++;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
