using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pause_Menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public AudioMixer audioMixer;

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale=0;//游戏暂停
    }
    public void closePM()
    {
        PauseMenu.SetActive(false);
        Time.timeScale=1;
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume",value);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
