using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMars()
    {
        PublicVariables.scenetoload = SceneToLoad.Mars;
        SceneManager.LoadScene("Loading");
    }

    public void LoadVesta()
    {
        PublicVariables.scenetoload = SceneToLoad.Vesta;
        SceneManager.LoadScene("Loading");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}