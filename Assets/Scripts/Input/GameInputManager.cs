using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    private GameInputAction gameInputAction;
    
    private void Awake()
    {
        gameInputAction = new();
        gameInputAction.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized() => gameInputAction.Player.Move.ReadValue<Vector2>();

    public bool IsShiftPressed() => gameInputAction.Player.Sprint.IsPressed();
}
