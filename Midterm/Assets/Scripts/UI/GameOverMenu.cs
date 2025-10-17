using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    //Send to Title
    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    //Exit Game
    public void QuitGame()
    {
        #if !UNITY_EDITOR
            Application.Quit();
        #elif UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
