using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectRaceCourse : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;

    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        StartCoroutine(LoadLevel(levelName));
    }

    public IEnumerator LoadLevel(string levelName)
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
        transitionAnim.SetTrigger("End");
        yield return null;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
