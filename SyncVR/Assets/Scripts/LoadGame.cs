using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadScene(string str)
    {
        SceneManager.LoadScene(str);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
