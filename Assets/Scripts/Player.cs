using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed = 20;
    [SerializeField] private GameInputManager gameInputManager;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject hitVFXPrefab;
    
    private Vector3 cursorDirection;

    private Dictionary<int, AbilitySO> abilities;
    private AbilitySO[] playerAbilitiesList;

    private void Awake()
    {
        Instance = this;
        
        playerAbilitiesList = Resources.LoadAll<AbilitySO>("");
        
        abilities = new Dictionary<int, AbilitySO>();
        foreach (AbilitySO ability in playerAbilitiesList)
        {
            abilities.Add(ability.ID, ability);
            ability.SetState(AbilitySO.State.Ready);
        }
    }

    private void Update()
    {
        HandleMovement();
        RaycastToCursor();
        
        foreach (AbilitySO ability in abilities.Values)
            ability.DecreaseCooldown();
    }

    public void OnShoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitinfo))
        {
            if (hitinfo.collider.tag == "Target")
            {
                Transform target = hitinfo.collider.transform;
                target.position += transform.forward * 0.2f;

                GameObject vfx = Instantiate(hitVFXPrefab, hitinfo.point, Quaternion.identity);
                Destroy(vfx, 2);
            }
        }
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
            characterController.Move(sprintSpeed * Time.deltaTime * moveVec3);
        }
        else
        {
            animator.SetBool("Sprint", false);
            characterController.Move(moveSpeed * Time.deltaTime * moveVec3);
        }
    }
    
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

    public void PlayAbility(int abilityID)
    {
        if (abilities.TryGetValue(abilityID, out AbilitySO ability))
        {
            ability.Activate();
        }
    }

    public void SetAnimationTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
