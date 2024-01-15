using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
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
