using System.Collections.Generic;
using Core.Logger;
using Member.YDW.AgentSystem;
using Member.YDW.EventChannels;
using Member.YDW.EventStruct;
using UnityEngine;
using Action = Unity.AppUI.Redux.Action;
using Random = UnityEngine.Random;

namespace Member.YDW.CombatSystem
{
    public class RandomEventManager : MonoBehaviour
    {
        [Range(0,100)] [field: SerializeField] public float EventChance { get; private set; }

        [SerializeField] private TurnManagerPauseEvent pauseEvent;
        [SerializeField] private RandomEvent randomEvent;
        [SerializeField] private List<RandomEventSO> randomEvents;
        
        private void Awake()
        {
            randomEvent.OnEvent += HandleRandomEvent;
        }

        private void HandleRandomEvent(AgentType obj)
        {
            //obj는 적용시킬 대상.
            float rand = Random.Range(0, 100);
            if (rand > EventChance)
                return;
            
            Logging.Log("이벤트 발동!");
            if (randomEvents.Count == 0)
            {
                Logging.Log("등록된 이벤트가 없습니다.");
                return;
            }
            pauseEvent.Raise(true);
            int randomEventIndex = Random.Range(0, randomEvents.Count);
            RandomEventSO activateRandomEvent = randomEvents[randomEventIndex];
            
            activateRandomEvent.RandomEvent.ActiveEvent(HandleEndEvent());
        }

        private Action HandleEndEvent()
        {
            pauseEvent.Raise(false);
            return null;
        }

        private void OnDestroy()
        {
            randomEvent.OnEvent -= HandleRandomEvent;
        }
    }
}