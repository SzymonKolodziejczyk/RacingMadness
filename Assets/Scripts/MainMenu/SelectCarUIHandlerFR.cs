using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCarUIHandlerFR : MonoBehaviour
{
    [Header("Car prefab")]
    public GameObject carPrefab;

    [Header("Spawn on")]
    public Transform spawnOnTransform;

    public Text obj_text;
    public InputField display;

    bool isChangingCar = false;

    CarDataFR[] carDatasFR;
    //CarDataDM[] carDatasDM;

    int selectedCarIndex = 0;

    //Other components
    CarUIHandler carUIHandler = null;

    // Start is called before the first frame update
    void Start()
    {
        //Load the car data
        carDatasFR = Resources.LoadAll<CarDataFR>("CarDataFR/");
        //carDatasDM = Resources.LoadAll<CarDataDM>("CarDataDM/");

        obj_text.text = PlayerPrefs.GetString("user_name");

        StartCoroutine(SpawnCarCO(true));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnPreviousCar();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            OnNextCar();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSelectCar();
        }
    }

    public void OnPreviousCar()
    {
        if (isChangingCar)
            return;

        selectedCarIndex--;

        if (selectedCarIndex < 0)
            selectedCarIndex = carDatasFR.Length - 1;

        StartCoroutine(SpawnCarCO(true));
    }

    public void OnNextCar()
    {
        if (isChangingCar)
            return;

        selectedCarIndex++;

        if (selectedCarIndex > carDatasFR.Length - 1)
            selectedCarIndex = 0;

        StartCoroutine(SpawnCarCO(false));
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ChoosePlayerName()
    {
        obj_text.text = display.text;
        PlayerPrefs.SetString("user_name", obj_text.text);
        PlayerPrefs.Save();
    }

    public void OnSelectCar()
    {
        GameManager.instance.ClearDriversList();

        if (display.text == null || display.text == "") 
        {
            display.text = "Player";
        }
        
        GameManager.instance.AddDriverToList(1, display.text, carDatasFR[selectedCarIndex].CarUniqueID, false);

        //Create a new list of cars
        List<CarDataFR> uniqueCars = new List<CarDataFR>(carDatasFR);

        //Remove the car that player has selected
        uniqueCars.Remove(carDatasFR[selectedCarIndex]);

        string[] names = { "Freddy", "Eddy", "Teddy", "Buddy", "Luddy", "Puddy", "Muddy", "Daddy", "Maddy" };
        List<string> uniqueNames = names.ToList<string>();

        //Add AI drivers
        for (int i = 2; i < 9; i++)
        {
            string driverName = uniqueNames[Random.Range(0, uniqueNames.Count)];
            uniqueNames.Remove(driverName);

            CarDataFR carDataFR = uniqueCars[Random.Range(0, uniqueCars.Count)];
            uniqueCars.Remove(carDataFR);

            GameManager.instance.AddDriverToList(i, driverName, carDataFR.CarUniqueID, true);
        }

        SceneManager.LoadScene("SelectRaceCourseFreeRace");
    }

    IEnumerator SpawnCarCO(bool isCarAppearingOnRightSide)
    {
        isChangingCar = true;

        if (carUIHandler != null)
            carUIHandler.StartCarExitAnimation(!isCarAppearingOnRightSide);

        GameObject instantiatedCar = Instantiate(carPrefab, spawnOnTransform);

        carUIHandler = instantiatedCar.GetComponent<CarUIHandler>();
        carUIHandler.SetupCarFR(carDatasFR[selectedCarIndex]);
        carUIHandler.StartCarEntranceAnimation(isCarAppearingOnRightSide);

        yield return new WaitForSeconds(0.4f);

        isChangingCar = false;
    }
}