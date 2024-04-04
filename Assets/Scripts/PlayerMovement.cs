using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Declares the speed variable with a public access modifier and initializes it with a value of 5.
    private Vector3 targetPosition;
    private bool isMoving = false;

    private Animator animator; // Reference to the Animator component

    void Start()
    {
        // Cache the Animator component at start.
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }

        if (isMoving)
        {
            MovePlayer();
        }
    }

    void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z; // Keep player at the same z-depth.
        isMoving = true;
    }

    void MovePlayer()
    {
        // Calculate the direction vector without the Y axis.
        Vector2 direction = (targetPosition - transform.position).normalized;

        // Set the moveX and moveY parameters based on the direction to the target position.
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);

        // Move the player towards the target position.
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the player has reached close enough to the target position and stop moving if so.
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) // A small threshold to stop the movement.
        {
            // Stop moving and reset moveX and moveY to 0 because the player has stopped.
            isMoving = false;
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 0);
        }
    }
}
