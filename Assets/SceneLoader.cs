using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public void NextScene()
    {
        SceneManager.LoadScene("Loading");
        SceneManager.LoadScene("Scene");

    }

    public void LoadMars()
    {
        PublicVariables.scenetoload = SceneToLoad.Mars;
        SceneManager.LoadScene("Loading");
        SceneManager.LoadScene("Scene");
        System.Threading.Thread.Sleep(1000);

    }

    public void LoadVesta()
    {
        PublicVariables.scenetoload = SceneToLoad.Vesta;
        SceneManager.LoadScene("Loading");
        SceneManager.LoadScene("Scene");
        System.Threading.Thread.Sleep(1000);
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