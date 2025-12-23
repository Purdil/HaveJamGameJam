using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Last : MonoBehaviour
{

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
        SceneManager.LoadScene("LobbyScene");
    }


    private void OnClickButton(Scene s, LoadSceneMode m)
    {
        SoundManager.Instance.PlaySound("Click");
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
