using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorUnlock : MonoBehaviour
{
    public GameObject interactionPrompt; // Assign a UI prompt that instructs to press E
    public string nextSceneName = "HallwayScene"; // Name of the next scene to load

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && FindObjectOfType<Inventory>().HasKey)
        {
            interactionPrompt.SetActive(true); // Show prompt to open the door
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
            SceneManager.LoadScene(nextSceneName); // Load the next scene
        }
    }
}
