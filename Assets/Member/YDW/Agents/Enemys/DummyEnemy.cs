namespace Member.YDW.Agents.Enemys
{
    public class DummyEnemy : AbstractEnemy
    {
        private void Update()
        {
        }

        public override void StartTurn()
        {
            base.StartTurn();
            OnTurnEnd = true;
        }

        public override void Attack()
        {
            
        }
    }
}