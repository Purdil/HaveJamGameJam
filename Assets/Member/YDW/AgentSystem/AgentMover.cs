using UnityEngine;

namespace Member.YDW.AgentSystem
{
    public class AgentMover : MonoBehaviour , IAgentComponent
    {
        private Agent _owner;
        
        private Rigidbody2D _rigidbody;
        public void Initialize(Agent owner)
        {
            _owner = owner;
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        
        public void SetDestination(Vector3 destination)
        {
            _rigidbody.MovePosition(_owner.transform.position + destination);
        }
    }
}