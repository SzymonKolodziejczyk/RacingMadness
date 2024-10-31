using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundSource;

    public static AudioManager instance;
    public AudioClip background;
    public AudioClip background2;

    // Create a dictionary to store the audio clips for each scene
    private Dictionary<string, AudioClip> sceneMusic = new Dictionary<string, AudioClip>();

    // Store the current playback position of the music
    private float currentPlaybackPosition = 0f;

    private void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this AudioManager
        instance = this;

        // Don't destroy the AudioManager when loading a new scene
        DontDestroyOnLoad(gameObject);

        // Load the audio clips for each scene
        LoadSceneMusic();

        // Subscribe to the SceneManager sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // Play the background music for the initial scene
        PlaySceneMusic(SceneManager.GetActiveScene().name);
    }

    private void LoadSceneMusic()
    {
        // Add the audio clips for each scene
        sceneMusic.Add("MainMenu", background);
        sceneMusic.Add("SelectGameMode", background);
        sceneMusic.Add("SelectYourCarFreerace", background2);
        sceneMusic.Add("SelectRaceCourseFreerace", background);
        // Add more scenes and audio clips as needed

        // You can also load the audio clips dynamically from resources or other sources
    }

    private void PlaySceneMusic(string sceneName)
    {
        {
            // Check if the scene has an associated audio clip
            if (sceneMusic.ContainsKey(sceneName))
            {
                // Check if the audio clip has changed
                if (backgroundSource.clip != sceneMusic[sceneName])
                {
                    // Reset the playback position to 0
                    currentPlaybackPosition = 0f;
                }

                // Set the background clip and play it
                backgroundSource.clip = sceneMusic[sceneName];

                // Set the playback position to the saved value
                backgroundSource.time = currentPlaybackPosition;

                backgroundSource.Play();
            }
            else
            {
                // If no audio clip is found, stop the background music
                backgroundSource.Stop();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Save the current playback position before loading the new scene
        currentPlaybackPosition = backgroundSource.time;

        // Play the background music for the newly loaded scene
        PlaySceneMusic(scene.name);
    }

    // Call this method from your scene transition code to change the music
    public void ChangeMusic(string sceneName)
    {
        // Stop the current background music
        backgroundSource.Stop();

        // Play the music for the new scene
        PlaySceneMusic(sceneName);
    }
}