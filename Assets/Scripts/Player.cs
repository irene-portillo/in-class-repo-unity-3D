using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed = 20;
    [SerializeField] private GameInputManager gameInputManager;
    [SerializeField] private Animator animator;

    private void Awake()
    {
    }

    private void Update()
    {
        HandleMovement();
    }

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
}
