using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUp : MonoBehaviour
{
    private string text;
    public TMP_Text textMeshPro;
    private float delay = 0.3f;

    public void Start()
    {
        text = textMeshPro.text.ToString();
        textMeshPro.text = " ";

        StartCoroutine(PlayTextUp(delay));
    }
    private IEnumerator PlayTextUp(float nmm)
    {
        int count = 0;

        while (count != text.Length)
        {
            if(count < text.Length)
            {
                textMeshPro.text += text[count].ToString();
                count++;
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
