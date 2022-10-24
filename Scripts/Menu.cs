using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Menu : MonoBehaviour
{
    public GameObject UI;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        PlayerPrefs.SetInt("Cherry", 0);
        PlayerPrefs.SetInt("Gem",0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void UIEable()
    {
        UI.SetActive(true);
    }

}
