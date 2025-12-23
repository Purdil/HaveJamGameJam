using System.Collections.Generic;
using System.Runtime.CompilerServices;
using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EscUi : MonoBehaviour
{
    public void Countinue()
    {
        SceneManager.LoadScene("TitleScene");
        SoundManager.Instance.PlaySound("Click");
    }

    public void OnEnd()
    {
        Application.Quit();
        SoundManager.Instance.PlaySound("Click");
    }
    public void GoLobby()
    {
        SceneManager.LoadScene("LobbyScene");
        SoundManager.Instance.PlaySound("Click");
    }
    public void GoBattle()
    {
        SceneManager.LoadScene("CombatScene");
        SoundManager.Instance.PlaySound("Click");
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
