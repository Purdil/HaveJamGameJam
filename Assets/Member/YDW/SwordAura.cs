using Core.Logger;
using Core.PoolSystem;
using Member.YDW.EventStruct;
using Member.YDW.HealthSystem;
using UnityEngine;

namespace Member.YDW
{
    public class SwordAura : MonoBehaviour, IPoolable
    {
        [SerializeField] private TurnManagerPauseEvent pauseEvent;
        [SerializeField] private float speed;
        private bool _isActive = false;
        public PoolableSO PoolableSO { get; private set; }

        private Rigidbody2D _rigid;
        
        private Vector2 _moveDir;
        
        private int _currentHealth;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        public void Initialize(int damage) //오우너 받아서 사라질 때, OnTurnEnd 실행시키기.
        {
            _isActive = true;
            _currentHealth = damage;
            _moveDir = (new Vector2(1, transform.position.y) - (Vector2)transform.position).normalized;
            pauseEvent.Raise(true);
        }
        
        public void SettingSO(PoolableSO poolableSO)
        {
            PoolableSO = poolableSO;
        }

        private void Update()
        {
            if(_isActive && _moveDir != Vector2.zero)
                _rigid.linearVelocity =  _moveDir * speed;
            else
                _rigid.linearVelocity = Vector2.zero;
        }

        public void OnPopObject()
        {
            
        }

        public void OnPushObject()
        {
           _isActive = false;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            Logging.Log("SwordAura Apply Damage");
            if (_isActive && other.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_currentHealth, out int overDamage);
                _currentHealth = overDamage;
                if (_currentHealth <= 0)
                {
                    _moveDir =  Vector2.zero;
                    pauseEvent.Raise(false);
                    PoolManager.Instance.Factory(PoolableSO).Push(this);
                }
            }
            
        }
    }
}