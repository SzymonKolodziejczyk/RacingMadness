using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCarUIHandlerDM : MonoBehaviour
{
    [Header("Car prefab")]
    public GameObject carPrefab;

    [Header("Spawn on")]
    public Transform spawnOnTransform;

    public Text obj_text;
    public InputField display;

    bool isChangingCar = false;

    CarDataDM[] carDatasDM;

    int selectedCarIndex = 0;

    //Other components
    CarUIHandler carUIHandler = null;

    // Start is called before the first frame update
    void Start()
    {
        //Load the car data
        carDatasDM = Resources.LoadAll<CarDataDM>("CarDataDM/");

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
            selectedCarIndex = carDatasDM.Length - 1;

        StartCoroutine(SpawnCarCO(true));
    }

    public void OnNextCar()
    {
        if (isChangingCar)
            return;

        selectedCarIndex++;

        if (selectedCarIndex > carDatasDM.Length - 1)
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
        
        GameManager.instance.AddDriverToList(1, display.text, carDatasDM[selectedCarIndex].CarUniqueID, false);

        //Create a new list of cars
        List<CarDataDM> uniqueCars = new List<CarDataDM>(carDatasDM);

        //Remove the car that player has selected
        uniqueCars.Remove(carDatasDM[selectedCarIndex]);

        string[] names = { "Freddy", "Eddy", "Teddy", "Buddy", "Luddy", "Puddy", "Muddy", "Daddy", "Maddy" };
        List<string> uniqueNames = names.ToList<string>();

        //Add AI drivers
        for (int i = 2; i < 9; i++)
        {
            string driverName = uniqueNames[Random.Range(0, uniqueNames.Count)];
            uniqueNames.Remove(driverName);

            CarDataDM carDataDM = uniqueCars[Random.Range(0, uniqueCars.Count)];
            uniqueCars.Remove(carDataDM);

            GameManager.instance.AddDriverToList(i, driverName, carDataDM.CarUniqueID, true);
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
        carUIHandler.SetupCarDM(carDatasDM[selectedCarIndex]);
        carUIHandler.StartCarEntranceAnimation(isCarAppearingOnRightSide);

        yield return new WaitForSeconds(0.4f);

        isChangingCar = false;
    }
}
