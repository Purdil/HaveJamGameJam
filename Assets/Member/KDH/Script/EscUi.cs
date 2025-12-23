using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EscUi : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject tars;


    public void Countinue()
    {
        //SoundManager.Instance.PlaySound("Click");
        SceneManager.sceneLoaded += OnClickButton;
        SceneManager.LoadScene("TitleScene");
    }

    public void OnEnd()
    {
        SoundManager.Instance.PlaySound("Click");
        Application.Quit();
    }
    public void GoLobby()
    {
        Debug.Log("[EscUI] ¼Ò¸®!");
        // SoundManager.Instance.PlaySound("Click");
        SceneManager.sceneLoaded += OnClickButton;
        StartCoroutine(GoPlus());
    }
    public IEnumerator GoPlus()
    {
        tars.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnClick()
    {
        SoundManager.Instance.PlaySound("Click");
        panel.SetActive(true);
    }

    private void OnClickButton(Scene s, LoadSceneMode m)
    {
        SoundManager.Instance?.PlaySound("Click");
        SceneManager.sceneLoaded -= OnClickButton;
    }

    public void GoBattle()
    {

        //SoundManager.Instance.PlaySound("Click");
        SceneManager.sceneLoaded += OnClickButton;
        SceneManager.LoadScene("CombatScene");
    }
    public void GoDebt()
    {
        //SceneManager.LoadScene("ShopScene");
        //SoundManager.Instance.PlaySound("Click");
    }
    public void GoShop()
    {
        // SceneManager.LoadScene("DebtScene");
        //SoundManager.Instance.PlaySound("Click");
    }
}
