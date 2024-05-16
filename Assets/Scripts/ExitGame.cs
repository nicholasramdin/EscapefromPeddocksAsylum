using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    // This method will be called when the ExitGame button is clicked
    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
        // If running in the editor, stop playing the scene
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
