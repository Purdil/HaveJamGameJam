using UnityEngine.InputSystem;

namespace Member.YDW.Agents.Enemys
{
    public class TestEnemy : AbstractEnemy
    { 
        private void Update()
        {
            #region Test

            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                OnTurnEnd = true;
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                ApplyDamage(1000 , out _);
            }
            

            #endregion
        }
    }
}