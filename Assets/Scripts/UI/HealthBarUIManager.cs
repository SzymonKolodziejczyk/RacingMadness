using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIManager : MonoBehaviour
{
    GameObject player;
    [SerializeField] public Image healthBar;
    [SerializeField] private float fillSpeed;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        
        if (player != null) {
                healthBar.fillAmount = Mathf.Clamp(player.GetComponent<HealthSystem>().health / player.GetComponent<HealthSystem>().maxHealth, fillSpeed, 1);
        }
    }
}
