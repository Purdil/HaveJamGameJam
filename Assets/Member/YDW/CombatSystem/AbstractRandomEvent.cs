using Unity.AppUI.Redux;
using UnityEngine;

namespace Member.YDW.CombatSystem
{
    public abstract class AbstractRandomEvent : MonoBehaviour
    {
        public abstract void ActiveEvent(Action callback);

    }
}