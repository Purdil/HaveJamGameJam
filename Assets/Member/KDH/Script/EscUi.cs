using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EscUi : MonoBehaviour
{
    public void Countinue()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnEnd()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SettingToggle();
        //}
        Application.Quit();
    }
}
