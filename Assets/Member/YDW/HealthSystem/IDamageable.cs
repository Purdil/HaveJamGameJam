namespace Member.YDW.HealthSystem
{
    public interface IDamageable
    {
        public void ApplyDamage(int damage, out int overDamage);
    }
}