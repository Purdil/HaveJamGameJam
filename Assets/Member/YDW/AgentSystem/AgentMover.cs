using Member.YDW.AnimationSystem;
using UnityEngine;

namespace Member.YDW.AgentSystem
{
    public class AgentMover : MonoBehaviour , IAgentComponent
    {
        [SerializeField] private AnimParamSO velocityXParam;
        [SerializeField] private float speed;
        private Agent _owner;
        private Rigidbody2D _rigidbody;
        private IAgentRenderer _renderer;
        private Vector2 moveDir;
        private Vector2 destination;

        public bool IsMoveEnt { get; private set; }

        public void Initialize(Agent owner)
        {
            if (owner != null)
            {
                _owner = owner;
                _renderer = _owner.GetCompo<IAgentRenderer>();
            }
            _rigidbody = GetComponentInParent<Rigidbody2D>();
                
        }

        private void FixedUpdate()
        {
            if (!ChackArrive() && moveDir !=  Vector2.zero)
            {
                if(_renderer != null)
                    _renderer.SetParam(velocityXParam,_rigidbody.linearVelocity.x);
                _rigidbody.linearVelocity = moveDir.normalized * speed;
            }
            else
            {
                if(_renderer != null)
                    _renderer.SetParam(velocityXParam,0f);
                _rigidbody.linearVelocity = Vector3.zero;
                moveDir = Vector2.zero;
                destination = Vector2.zero;
            }
        }

        private bool ChackArrive()
        {
            if (Vector3.Distance(_owner.transform.position, destination) <= 0.1f)
                return true;
            return false;
        }


        public void SetDestination(Vector3 destination)
        {
            this.destination = destination;
            moveDir = destination - _owner.transform.position;
            moveDir.Normalize();
        }
    }
}