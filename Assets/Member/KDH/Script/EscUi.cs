using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class EscUi : MonoBehaviour
{
    //[SerializeField] private GameObject gameUi;


    //private bool isOpen;

    public void OnEnd()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    SettingToggle();
        //}
        Application.Quit();
    }

    //private void SettingToggle()
    //{
    //    isOpen = !isOpen;
    //    SetActive(isOpen);

    //    Time.timeScale = isOpen ? 0f : 1f;
    //}
    //private void SetActive(bool isOpen)
    //{
    //    gameUi.SetActive(isOpen);
    //}
}
