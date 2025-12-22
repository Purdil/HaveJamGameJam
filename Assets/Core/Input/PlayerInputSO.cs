using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "Scriptable Objects/PlayerInputSO")]
public class PlayerInputSO : ScriptableObject, PlayerInput.IPlayerActions
{
    private PlayerInput input;

    public event Action MouseClick;
    private void Awake()
    {
        if (input == null)
        {
            input = new PlayerInput();
            input.Player.SetCallbacks(this);
            
        }
        
        input.Player.Enable();
    }

    private void OnDestroy()
    {
        input.Player.Disable();
    }


    public void OnMouseClick(InputAction.CallbackContext context)
    {
        MouseClick?.Invoke();
    }
    
    
    
    
    
}
