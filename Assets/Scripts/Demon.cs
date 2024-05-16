using UnityEngine;

public class Demon : MonoBehaviour
{
    public GameObject uiPanel;  // Assign the UI panel that contains the Demon text
    public GameObject promptText;  // Assign the UI text element for the interaction prompt
    private bool isPlayerNear = false;  // To check if player is near the Demon

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ToggleUIPanel();  // Toggle the visibility of the UI panel
            promptText.SetActive(false);  // Optionally hide prompt when UI panel is active
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;  // Player has entered the trigger
            promptText.SetActive(true);  // Show the prompt text
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;  // Player has left the trigger
            promptText.SetActive(false);  // Hide the prompt text
            uiPanel.SetActive(false);  // Ensure the panel is hidden when player is not nearby
        }
    }

    private void ToggleUIPanel()
    {
        // Toggle the active state of the panel
        uiPanel.SetActive(!uiPanel.activeSelf);
    }
}
