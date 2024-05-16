using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    public void LoadCellScene()
    {
        SceneManager.LoadScene("CellScene");
    }

    public void LoadHowToPlayScene()
    {
        SceneManager.LoadScene("HowtoPlayScene");
    }
}
