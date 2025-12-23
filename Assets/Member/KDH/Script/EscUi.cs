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
        Application.Quit();
    }
    public void GoLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public void GoBattle()
    {
        SceneManager.LoadScene("CombatScene");
    }
    public void GoDebt()
    {
        //SceneManager.LoadScene("ShopScene");
    }
    public void GoShop()
    {
       // SceneManager.LoadScene("DebtScene");
    }
}
