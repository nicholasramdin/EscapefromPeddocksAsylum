using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Vector3 targetPosition;
    public float speed = 5.0f;  // Adjust speed as needed.
    public LayerMask collisionLayer;  // Define which layers constitute obstacles.

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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    void MovePlayer()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > 0.1f)
        {
            // Check if the path to the target position is clear using raycasting
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(direction.x, direction.y), distance, collisionLayer);
            Debug.Log("Raycast hit: " + hit.collider);

            if (hit.collider == null)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                UpdateWalkingAnimations(true);
            }
            else
            {
                // Stop movement when hitting an obstacle, adjust the position to be slightly before the hit point
                Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, hitPoint - (direction * 0.05f), speed * Time.deltaTime);
                UpdateWalkingAnimations(false);
            }
        }
        else
        {
            UpdateWalkingAnimations(false);
        }
    }

    void UpdateWalkingAnimations(bool isMoving)
    {
        if (isMoving)
        {
            animator.SetBool("isWalkingLeft", targetPosition.x < transform.position.x);
            animator.SetBool("isWalkingRight", targetPosition.x > transform.position.x);
            animator.SetBool("isWalkingUp", targetPosition.y > transform.position.y);
            animator.SetBool("isWalkingDown", targetPosition.y < transform.position.y);
        }
        else
        {
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", false);
        }
    }
}
