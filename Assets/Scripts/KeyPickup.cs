using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject interactionPrompt; // Assign a UI prompt that instructs to press E

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionPrompt.SetActive(true); // Show prompt
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionPrompt.SetActive(false); // Hide prompt
        }
    }

    private void Update()
    {
        if (interactionPrompt.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<Inventory>().HasKey = true; // Assume Inventory script exists
            gameObject.SetActive(false); // Disable the key object
        }
    }
}
