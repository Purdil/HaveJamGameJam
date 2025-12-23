using Member.YDW.AgentSystem;
using Member.YDW.CombatSystem;
using Member.YDW.EventChannels;
using Member.YDW.EventStruct;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Member.YDW
{
    public class CombatEndUi : MonoBehaviour
    {
        [SerializeField] private TurnStatEvent turnStateEvent;

        [SerializeField] private TextMeshProUGUI playerNameText;
        private void Awake()
        {
            gameObject.SetActive(false);
            turnStateEvent.OnEvent += HandleGameEnd;
        }

        private void HandleGameEnd(CombatSettingValue obj)
        {
            if (obj.State == TurnState.CombatEnd && obj.AgentType == AgentType.Player)
            {
                gameObject.SetActive(true);
                playerNameText.text = "Enemy Win!";
                Time.timeScale = 0;
            }
            else if (obj.State == TurnState.CombatEnd && obj.AgentType == AgentType.Enemy)
            {
                gameObject.SetActive(true);
                playerNameText.text = "Player Win!";
                Time.timeScale = 0;
            }
        }

        public void ReturnScene()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(2);
        }
    }
}