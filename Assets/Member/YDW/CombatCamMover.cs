using UnityEngine;

namespace Member.YDW
{
    public class CombatCamMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private PlayerInputSO playerInput;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = playerInput.MoveDir *  moveSpeed;
        }
    }
}