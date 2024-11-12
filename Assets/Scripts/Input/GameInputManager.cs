using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    private GameInputAction gameInputAction;

    [SerializeField] private Player player;
    
    private void Awake()
    {
        gameInputAction = new();
        gameInputAction.Player.Enable();
        
        gameInputAction.Player.Shoot.performed += OnShootPerformed;
        gameInputAction.Player.Punch.performed += OnPunchPerformed;
    }

    private void OnPunchPerformed(InputAction.CallbackContext obj)
    {
        player.PlayAbility(0);
    }

    private void OnShootPerformed(InputAction.CallbackContext obj)
    {
        player.OnShoot();
    }

    public Vector2 GetMovementVectorNormalized() => gameInputAction.Player.Move.ReadValue<Vector2>();

    public bool IsShiftPressed() => gameInputAction.Player.Sprint.IsPressed();
}
