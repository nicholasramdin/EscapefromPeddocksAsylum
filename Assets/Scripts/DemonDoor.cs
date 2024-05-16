using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DemonDoor : MonoBehaviour
{
    public GameObject uiPanel;  // Assign the UI panel that contains the dialogue text
    public GameObject promptText;  // Assign the UI text element for the interaction prompt
    public Button openButton;  // Assign the UI button for opening the door
    public Button leaveButton;  // Assign the UI button for leaving
    public Animator demonAnimator;  // Assign the Animator for the demon animation
    public CameraShake cameraShake;  // Reference to the CameraShake script

    private bool isPlayerNear = false;  // To check if the player is near the demon door

    // Set the duration to wait for the animation to finish (in seconds)
    public float animationWaitDuration = 4.0f;
    public float shakeDuration = 0.5f;  // Duration of the camera shake
    public float shakeMagnitude = 0.1f;  // Magnitude of the camera shake

    void Start()
    {
        // Initially deactivate all UI elements
        promptText.SetActive(false);
        uiPanel.SetActive(false);
        openButton.gameObject.SetActive(false);
        leaveButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialogue();  // Show the dialogue panel with options
            promptText.SetActive(false);  // Hide the interaction prompt
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
            uiPanel.SetActive(false);  // Ensure the panel is hidden when the player is not nearby
            openButton.gameObject.SetActive(false);
            leaveButton.gameObject.SetActive(false);
        }
    }

    private void ShowDialogue()
    {
        // Show the dialogue panel
        uiPanel.SetActive(true);

        // Show the buttons
        openButton.gameObject.SetActive(true);
        leaveButton.gameObject.SetActive(true);

        // Add button listeners
        openButton.onClick.RemoveAllListeners();  // Prevent multiple listeners
        leaveButton.onClick.RemoveAllListeners();  // Prevent multiple listeners

        openButton.onClick.AddListener(OpenDoor);
        leaveButton.onClick.AddListener(LeaveDoor);
    }

    private void OpenDoor()
    {
        // Trigger demon animation
        demonAnimator.SetTrigger("Appear");

        // Start coroutine to wait for the animation to finish
        StartCoroutine(WaitForAnimation(animationWaitDuration));
    }

    private void LeaveDoor()
    {
        // Trigger camera shake and then transition to game over
        StartCoroutine(ShakeAndLoadScene());
    }

    // Coroutine to wait for the specified duration
    private IEnumerator WaitForAnimation(float waitTime)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(waitTime);

        // Load the game over scene
        SceneManager.LoadScene("GameOverScene");
    }

    // Coroutine to shake the camera and then transition to the game over scene
    private IEnumerator ShakeAndLoadScene()
    {
        // Trigger camera shake
        StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));

        // Wait for the shake duration to finish
        yield return new WaitForSeconds(shakeDuration);

        // Load the game over scene
        SceneManager.LoadScene("GameOverScene");
    }
}
