using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectGameModeMenu : MonoBehaviour
{
    public GameModes selectedMode;
    
   public void FreeRaceButton()
    {
        //skillLevelSlider.value = PlayerPrefs.GetFloat("SkillLevel");
        Debug.Log("Free Race mode was chosen!");
        //GameManager.instance.OnModeSelected(GameModes.FreeRacing);
        //GameManager.GameModes = FreeRacing;
        //PlayerPrefs.SetFloat("GameModes", GameModes.FreeRacing);
        GameManager.instance.OnModeSelected(GameModes.FreeRacing);
        SceneManager.LoadScene("SelectYourCarFreerace");
    }

    public void DeathmatchButton()
    {
        Debug.Log("Deathmatch mode was chosen!");
        GameManager.instance.OnModeSelected(GameModes.DeathMatch);
        SceneManager.LoadScene("SelectYourCarDeathmatch");
    }

   /*void OnButtonClick()
    {
        GameManager.instance.OnModeSelected(selectedMode);
    }*/

   public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
