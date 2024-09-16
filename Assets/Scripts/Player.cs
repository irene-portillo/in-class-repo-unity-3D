using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed = 20;
    [SerializeField] private GameInputManager gameInputManager;
    [SerializeField] private Animator animator;
    
    private Vector3 cursorDirection;
    
    private void Awake()
    {
    }

    private void Update()
    {
        HandleMovement();
        RaycastToCursor();
    }

    /// <summary>
    /// Handle WASD player movement on the xz plane
    /// </summary>
    private void HandleMovement()
    {
        Vector2 moveVec2 = gameInputManager.GetMovementVectorNormalized();
        
        animator.SetBool("Walk", moveVec2 != Vector2.zero);
        
        Vector3 moveVec3 = new Vector3(moveVec2.x, 0, moveVec2.y);

        if (gameInputManager.IsShiftPressed())
        {
            animator.SetBool("Sprint", true);
            transform.Translate(sprintSpeed * Time.deltaTime * moveVec3);
        }
        else
        {
            animator.SetBool("Sprint", false);
            transform.Translate(moveSpeed * Time.deltaTime * moveVec3);
        }
    }
    
    /// <summary>
    /// Raycasts from player position to mouse position and outputs data to hit.
    /// </summary>
    private void RaycastToCursor()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.down, transform.position.y);

        // intersect camera ray with a plane beneath player's feet
        if (groundPlane.Raycast(mouseRay, out float distance))
        {
            Vector3 cursorWorldPosition = mouseRay.GetPoint(distance);
            cursorDirection = (cursorWorldPosition - transform.position).normalized;
            
            // update forward vector with direction:
            transform.forward = cursorDirection;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, transform.position + cursorDirection * 10);
    }
}
