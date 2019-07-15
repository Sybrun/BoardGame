using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GoToGame()
    {
        // Laadt de "Game" scene.
        SceneManager.LoadScene("Game");
    }

    public void GoToMenu()
    {
        //Laadt de "Menu" scene.
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        //Sluit het spel af.
        Application.Quit();
    }
}
