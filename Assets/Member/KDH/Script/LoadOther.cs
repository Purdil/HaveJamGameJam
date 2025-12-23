using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOther : MonoBehaviour
{
    public void OnLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public void OnBattleScene()
    {
        SceneManager.LoadScene("");
    }
    public void OnShopScene()
    {
        SceneManager.LoadScene("");
    }
    public void Debt()
    {
        SceneManager.LoadScene("");
    }
}
