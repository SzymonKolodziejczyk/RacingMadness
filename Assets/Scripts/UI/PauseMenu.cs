using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using System.Threading.Tasks;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public static bool isPaused;

    GameStates RunningCheck;

    [SerializeField] RectTransform pausePanelRect;
    [SerializeField] float tweenDuration;
    [SerializeField] public CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RunningCheck = GameManager.instance.GetGameState();
        //GameManager ML = new GameStates();
        if(RunningCheck == GameStates.running)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        foreach (AudioSource a in audios)
        {
            a.mute = true;
        }

        PausePanelIntro();
    }

    public async void ResumeGame()
    {
        await PausePanelOutro();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        
        foreach (AudioSource a in audios)
        {
            a.mute = false;
        }
    }

    void PausePanelIntro()
    {
        //canvasGroup.DOFade(0.3, tweenDuration).SetUpdate(true);
        pausePanelRect.transform.localPosition = new Vector3(0f,-1000f,0f);
        pausePanelRect.DOAnchorPos(new Vector2(0f, 0f), tweenDuration, false).SetEase(Ease.OutElastic).SetUpdate(true);
    }

    async Task PausePanelOutro()
    {
        //canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        //await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        pausePanelRect.transform.localPosition = new Vector3(0f,0f,0f);
        await pausePanelRect.DOAnchorPos(new Vector2(0f, -1000f), tweenDuration, false).SetEase(Ease.InOutQuint).SetUpdate(true).AsyncWaitForCompletion();
    }
}
