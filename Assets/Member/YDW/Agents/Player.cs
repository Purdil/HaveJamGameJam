using Member.YDW.AgentSystem;
using Member.YDW.HealthSystem;
using UnityEngine.InputSystem;

namespace Member.YDW.Agents
{
    public class Player : Agent
    {
        public IDamageable target;
        private void Update()
        {
            #region Test

            if (Keyboard.current.nKey.wasPressedThisFrame)
            {
                OnTurnEnd = true;
            }

            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                ApplyDamage(1000);
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                target.ApplyDamage(10);
            }
            

            #endregion
        }
    }
}