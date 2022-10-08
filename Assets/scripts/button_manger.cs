using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button_manger : MonoBehaviour
{
    public void Loadscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // opens next scene in the array
    }
    public void exit()
    {
        Application.Quit();
    }
    public void credit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); // opens next scene in the array
    }
}
