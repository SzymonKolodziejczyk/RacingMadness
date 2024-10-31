using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;

    [SerializeField] RectTransform pausePanelRect;
    [SerializeField] float tweenDuration;

    public async void ResumeGame()
    {
        await ResumeGameAnimation();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        
        foreach (AudioSource a in audios)
        {
            a.mute = false;
        }
    }

    public void OnRaceAgain()
    {
        Debug.Log("Let's Race Again!");
        Time.timeScale = 1f;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        
        foreach (AudioSource a in audios)
        {
            a.mute = false;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   public void BackToMainMenu()
   {
        Debug.Log("Back to Main Menu!");
        Time.timeScale = 1f;
        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        
        foreach (AudioSource a in audios)
        {
            a.mute = false;
        }

        SceneManager.LoadScene("MainMenu");
   }

   async Task ResumeGameAnimation()
    {
        pausePanelRect.transform.localPosition = new Vector3(0f,0f,0f);
        await pausePanelRect.DOAnchorPos(new Vector2(0f, -1000f), tweenDuration, false).SetEase(Ease.InOutQuint).SetUpdate(true).AsyncWaitForCompletion();
    }
}
