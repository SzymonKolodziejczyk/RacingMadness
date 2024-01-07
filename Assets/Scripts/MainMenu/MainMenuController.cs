using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
   public void StartButton()
   {
      Debug.Log("Choose your car!");
      SceneManager.LoadScene("SelectYourCar");
   }

   public void QuitGame()
   {
      Debug.Log("Quitting!");
      Application.Quit();
   }
}
