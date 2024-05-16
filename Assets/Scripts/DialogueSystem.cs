using UnityEngine;
using UnityEngine.UI;

public class DemonDoorInteraction : MonoBehaviour
{
    public GameObject interactPromptObject;  // Assign the UI text element for the interaction prompt
    public GameObject dialoguePanel;  // Assign the UI panel that contains the dialogue
    public GameObject demonDoor;  // Assign the Demon Door GameObject

    private bool isPlayerNear = false;  // To check if the player is near the demon door

    void Start()
    {
        // Ensure the UI elements are initially hidden
        interactPromptObject.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Show the dialogue panel
            dialoguePanel.SetActive(true);
            interactPromptObject.SetActive(false);  // Hide the interaction prompt
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject == demonDoor)
        {
            isPlayerNear = true;
            interactPromptObject.SetActive(true);  // Show the prompt text
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject == demonDoor)
        {
            isPlayerNear = false;
            interactPromptObject.SetActive(false);  // Hide the prompt text
            dialoguePanel.SetActive(false);  // Ensure the dialogue panel is hidden
        }
    }
}
