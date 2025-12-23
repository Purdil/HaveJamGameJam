using Core.Logger;
using Member.YDW.EventChannels;
using Member.YDW.EventStruct;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.YDW.CombatSystem
{
    public class RandomEventManager : MonoBehaviour
    {
        [Range(0,100)] [field: SerializeField] public float EventChance { get; private set; }
        [SerializeField] private RandomEventInfoEvent infoEvent;
        [SerializeField] private TurnManagerPauseEvent pauseEvent;
        [SerializeField] private RandomEvent randomEvent;
        
        private void Awake()
        {
            randomEvent.OnEvent += HandleRandomEvent;
            gameObject.SetActive(false);
        }

        private void HandleRandomEvent(ICanHoldRandomEvent obj)
        {
            //obj는 적용시킬 대상.
            float rand = Random.Range(0, 100);
            if (rand > EventChance)
                return;
            
            Logging.Log("이벤트 발동!");
            pauseEvent.Raise(true);
            int randomEventIndex = Random.Range(0,5);
            switch (randomEventIndex)
            {
                case 0:
                    infoEvent.Raise("데미지 2배!");
                    obj.doubleDamage = true;
                    break;
                case 1:
                    infoEvent.Raise("데미지 절반!");
                    obj.halfDamage = true;
                    break;
                case 2:
                    infoEvent.Raise("데미지에 - 부호!");
                    obj.minusDamage = true;
                    break;
                case 3:
                    infoEvent.Raise("데미지에 + 부호!");
                    obj.plusDamage = true;
                    break;
                case 4:
                    infoEvent.Raise("룰렛 다시 돌리기!");
                    obj.reSpine = true;
                    break;
            }
            pauseEvent.Raise(false);
            
        }

        private void OnDestroy()
        {
            randomEvent.OnEvent -= HandleRandomEvent;
        }
    }
}