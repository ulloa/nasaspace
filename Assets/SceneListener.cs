using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneListener : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadSceneAsync("Scene");
    }
}
