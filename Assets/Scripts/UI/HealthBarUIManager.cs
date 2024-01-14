using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIManager : MonoBehaviour
{
    GameObject player;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private Image healthBar;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private Text healthCounter;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        lerpSpeed = 3f * Time.deltaTime;
        float TargetHealth = player.GetComponent<HealthSystem>().health / player.GetComponent<HealthSystem>().maxHealth;
        if (player != null) {
                healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, TargetHealth, lerpSpeed);
                healthCounter.text = TargetHealth * 100 + "%";
                healthBar.color = colorGradient.Evaluate(TargetHealth);
        }
    }
}
