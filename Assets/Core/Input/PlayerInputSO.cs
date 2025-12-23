using Core.Logger;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "Scriptable Objects/PlayerInputSO")]
public class PlayerInputSO : ScriptableObject, Control.IPlayerActions
{
    private Control input;

    [field: SerializeField] public Vector3 MoveDir { get; private set; }
    
    
    private void OnEnable()
    {
        if (input == null)
        {
            input = new Control();
            input.Player.SetCallbacks(this);
            
        }
        
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveDir = context.ReadValue<Vector2>();
        Logging.Log($"MoveDir: {MoveDir}");
    }
}
