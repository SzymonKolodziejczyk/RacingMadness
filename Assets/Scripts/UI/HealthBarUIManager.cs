using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIManager : MonoBehaviour
{
    public Image healthBar;
    GameObject player;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        
            if (player != null) {
                    healthBar.fillAmount = Mathf.Clamp(player.GetComponent<HealthAndRespawnCar>().health / player.GetComponent<HealthAndRespawnCar>().maxHealth, 0, 1);
            }
    }
}
