using Member.YDW.HealthSystem;
using UnityEngine;

namespace Member.YDW
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform healthBar;
        private AgentHealth healthSystem;

        private void Awake()
        {
            healthSystem = transform.parent.GetComponentInChildren<AgentHealth>();
        }

        private void OnEnable()
        {
            UpdateBar(0);
        }
        private void Start()
        {
            UpdateBar(0);
            healthSystem.OnDamaged += UpdateBar;
            healthSystem.OnHealed += UpdateBar;
        }

        private void UpdateBar(int amount)
        {


            float health = (float)healthSystem.Health / healthSystem.GetMaxHealth();
            healthBar.localScale = new Vector3(health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);


        }
    }
}