using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // This method will be called when the Play Game button is clicked
    public void PlayGame()
    {
        // Load the CellScene
        SceneManager.LoadScene("CellScene");
    }
}
