using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button_manger : MonoBehaviour
{
    public void Loadscene()
    {
        SceneManager.LoadScene(1); // opens next scene in the array
    }
    public void exit()
    {
        Application.Quit();
    }
    public void credit()
    {
        SceneManager.LoadScene(2); // 
    }
    public void retry()
    {
        SceneManager.LoadScene(1); // 
    }
    public void menu()
    {
        SceneManager.LoadScene(0); // 
    }
}
