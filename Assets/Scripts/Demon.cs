using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Demon : MonoBehaviour
{
    public GameObject uiPanel;  // Assign the UI panel that contains the Demon text
    public GameObject promptText;  // Assign the UI text element for the interaction prompt
    public Button attackButton;  // Assign the UI button for attacking the demon
    private bool isPlayerNear = false;  // To check if player is near the Demon
    private bool hasTalkedToDemon = false;  // To check if the player has talked to the demon

    void Start()
    {
        // Initially deactivate all UI elements
        promptText.SetActive(false);
        uiPanel.SetActive(false);
        attackButton.gameObject.SetActive(false);

        // Add listener to the attack button
        attackButton.onClick.AddListener(OnAttackButtonClick);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ToggleUIPanel();  // Toggle the visibility of the UI panel
            promptText.SetActive(false);  // Optionally hide prompt when UI panel is active

            // If the player talks to the demon, show the attack button
            if (!hasTalkedToDemon)
            {
                hasTalkedToDemon = true;
                attackButton.gameObject.SetActive(true);  // Show the attack button
            }
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

    private void OnAttackButtonClick()
    {
        // Load the GameOverFromHellScene when the attack button is clicked
        SceneManager.LoadScene("GameOverFromHellScene");
    }
}
