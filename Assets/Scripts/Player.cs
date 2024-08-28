using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInputManager gameInputManager;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 moveVec2 = gameInputManager.GetMovementVectorNormalized();
        Vector3 moveVec3 = new Vector3(moveVec2.x, 0, moveVec2.y);
        transform.Translate(moveSpeed * Time.deltaTime * moveVec3);
    }
}
