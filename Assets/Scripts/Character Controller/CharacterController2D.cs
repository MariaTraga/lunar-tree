using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 2f;
    

    Rigidbody2D rb;
    Animator animator;
    Vector2 movement;
    Vector2 interactPosition;
    public Vector2 lastPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
    }

    private void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        ChangeFacingDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = movement * movementSpeed;
    }

    private void ChangeFacingDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat("walkPositionX", rb.velocity.x);
        animator.SetFloat("walkPositionY", rb.velocity.y);

        if (horizontal == 1 || horizontal == -1)
        {
            animator.SetFloat("idlePositionY", 0);
            animator.SetFloat("idlePositionX", Input.GetAxisRaw("Horizontal"));
        }
        if (vertical != 0 && horizontal == 0)
        {
            animator.SetFloat("idlePositionX", 0);
            animator.SetFloat("idlePositionY", Input.GetAxisRaw("Vertical"));
        }

        SetFacingDirection(horizontal, vertical);
    }

    private void SetFacingDirection(float horizontal, float vertical)
    {
        if (vertical != 0 || horizontal != 0)
        {
            lastPosition = new Vector2(horizontal, vertical).normalized;
        }
    }

    public Collider2D[] CreateInteractArea(float offsetDistance, float sizeOfInteractableArea)
    {
        //Create interact collider position based on the characters position, facing direstion and an offset
        interactPosition = rb.position + lastPosition * offsetDistance;

        //Detect interactable by tools objects
        Collider2D[] interactableColliders = Physics2D.OverlapCircleAll(interactPosition, sizeOfInteractableArea);

        return interactableColliders;
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactPosition, sizeOfInteractableArea);
    }*/

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
        ChangeFacingDirection();
    }

}
