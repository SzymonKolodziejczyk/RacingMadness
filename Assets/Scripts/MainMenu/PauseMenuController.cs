using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        foreach (AudioSource a in audios)
        {
            a.Play();
        }
    }

    public void OnRaceAgain()
    {
        Debug.Log("Let's Race Again!");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   public void BackToMainMenu()
   {
      Debug.Log("Back to Main Menu!");
      Time.timeScale = 1f;
      SceneManager.LoadScene("MainMenu");
   }
}
