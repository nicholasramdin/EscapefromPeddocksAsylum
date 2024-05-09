using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Vector3 targetPosition;
    public float speed = 5.0f;  // Adjust speed as needed.

    void Start()
    {
        animator = GetComponent<Animator>();
        // Initialize target position at start to prevent unintentional movement
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }

        MovePlayer();
    }

    void SetTargetPosition()
    {
        // Get the position from the mouse click and set the z-coordinate to the player's current z-coordinate
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;
    }

    void MovePlayer()
    {
        // Move the player towards the target position only if the distance is significant
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            UpdateWalkingAnimations(true);
        }
        else
        {
            UpdateWalkingAnimations(false);
        }
    }

    void UpdateWalkingAnimations(bool isMoving)
    {
        // Update animation parameters based on the direction of movement
        if (isMoving)
        {
            animator.SetBool("isWalkingLeft", targetPosition.x < transform.position.x);
            animator.SetBool("isWalkingRight", targetPosition.x > transform.position.x);
            animator.SetBool("isWalkingUp", targetPosition.y > transform.position.y);
            animator.SetBool("isWalkingDown", targetPosition.y < transform.position.y);
        }
        else
        {
            // Reset all walking animations when the destination is reached or if not moving
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", false);
        }
    }
}

