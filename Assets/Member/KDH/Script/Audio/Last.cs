using System.Collections;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Last : MonoBehaviour
{
    public UnityEvent OnShopUiOpenEvent;

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
        Debug.Log("[EscUI] �Ҹ�!");
        // SoundManager.Instance.PlaySound("Click");
        SceneManager.sceneLoaded += OnClickButton;
        SceneManager.LoadScene("LobbyScene");
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
        SceneManager.sceneLoaded += OnClickButton;
    }
    public void GoShop()
    {
    }
}
