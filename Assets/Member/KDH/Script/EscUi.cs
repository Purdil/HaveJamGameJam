using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public void LoadMove()
    {
        SceneManager.LoadScene("StartScene");
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
