using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum GameModes { FreeRacing, DeathMatch }
public enum GameStates { countDown, running, raceOver, Deathmatch }

public class GameManager : MonoBehaviour
{
    //Static instance of GameManager so other scripts can access it
    public static GameManager instance = null;

    public GameModes selectedGameMode;
    private const string GameModeKey = "SelectedGameMode";
    public static GameModes chosenGameMode;
    public GameStates gameState = GameStates.countDown;

    // Racing-specific variables
    float raceStartedTime = 0;
    float raceCompletedTime = 0;
    List<DriverInfo> driverInfoList = new List<DriverInfo>();

    // Deathmatch-specific variables
    int defeatedAICount = 0;

    public event Action<GameManager> OnGameStateChanged;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        //selectedGameMode = chosenGameMode; // Set the game mode based on the stored value
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Supply dummy driver information for testing purposes
        driverInfoList.Add(new DriverInfo(1, "P1", 0, false));
    }

    void LevelStart()
    {
        gameState = GameStates.countDown;

        if(PlayerPrefs.HasKey(GameModeKey))
        {
            int savedValue = PlayerPrefs.GetInt(GameModeKey);
            selectedGameMode = (GameModes)savedValue;
        }

        Debug.Log("Level started");
        Debug.Log("The game mode is "+ selectedGameMode.ToString());
    }

    public GameStates GetGameState()
    {
        return gameState;
    }

    void ChangeGameState(GameStates newGameState)
    {
        if (gameState != newGameState)
        {
            gameState = newGameState;
            OnGameStateChanged?.Invoke(this);
        }
    }

    public void OnModeSelected(GameModes selectedMode)
    {
        selectedGameMode = selectedMode;
        PlayerPrefs.SetInt(GameModeKey, (int)selectedGameMode);
    }

    // Racing-specific methods
    public void StartRace()
    {
        CarAIHandler aIHandler = FindObjectOfType<CarAIHandler>();

        if (selectedGameMode != GameModes.FreeRacing)
        {
            Debug.LogError("Cannot start race in non-racing mode");
            return;
        }

        raceStartedTime = Time.time;
        ChangeGameState(GameStates.running);
    }

    public void CompleteRace()
    {
        if (selectedGameMode != GameModes.FreeRacing)
        {
            Debug.LogError("Cannot complete race in non-racing mode");
            return;
        }

        raceCompletedTime = Time.time;
        ChangeGameState(GameStates.raceOver);
    }

    // Deathmatch-specific methods
    public void StartDeathmatch()
    {
        if (selectedGameMode != GameModes.DeathMatch)
        {
            Debug.LogError("Cannot start deathmatch in non-deathmatch mode");
            return;
        }

        defeatedAICount = 0;
        ChangeGameState(GameStates.running);
    }

    public void DefeatAI()
    {
        if (selectedGameMode != GameModes.DeathMatch)
        {
            Debug.LogError("Cannot defeat AI in non-deathmatch mode");
            return;
        }

        defeatedAICount++;

        // Check if all AI opponents are defeated
        if (defeatedAICount == driverInfoList.Count - 1)
        {
            ChangeGameState(GameStates.raceOver);
        }
    }

    public float GetRaceTime()
    {
        if (gameState == GameStates.countDown)
            return 0;
        else if (gameState == GameStates.raceOver)
            return raceCompletedTime - raceStartedTime;
        else return Time.time - raceStartedTime;
    }

    //Driver information handling
    public void ClearDriversList()
    {
        driverInfoList.Clear();
    }

    public void AddDriverToList(int playerNumber, string name, int carUniqueID, bool isAI)
    {
        driverInfoList.Add(new DriverInfo(playerNumber, name, carUniqueID, isAI));
    }

    public void SetDriversLastRacePosition(int playerNumber, int position)
    {
        DriverInfo driverInfo = FindDriverInfo(playerNumber);
        driverInfo.lastRacePosition = position;
    }

    public void AddPointsToChampionship(int playerNumber, int points)
    {
        DriverInfo driverInfo = FindDriverInfo(playerNumber);

        driverInfo.championshipPoints += points;
    }

    DriverInfo FindDriverInfo(int playerNumber)
    {
        foreach (DriverInfo driverInfo in driverInfoList)
        {
            if (playerNumber == driverInfo.playerNumber)
                return driverInfo;
        }

        Debug.LogError($"FindDriverInfoBasedOnDriverNumber failed to find driver for player number {playerNumber}");

        return null;
    }

    public List<DriverInfo> GetDriverList()
    {
        return driverInfoList;
    }

    public void OnRaceStart()
    {
        Debug.Log("OnRaceStart");

        raceStartedTime = Time.time;

        ChangeGameState(GameStates.running);
    }
    public void OnRaceCompleted()
    {
        Debug.Log("OnRaceCompleted");

        raceCompletedTime = Time.time;

        ChangeGameState(GameStates.raceOver);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelStart();
    }

}
