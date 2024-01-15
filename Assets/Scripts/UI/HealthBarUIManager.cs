using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIManager : MonoBehaviour
{
    GameObject player;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float healthBarAmount;
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
                healthBarAmount = Mathf.Lerp(healthBar.fillAmount, TargetHealth, lerpSpeed);
                healthBar.fillAmount = Mathf.Clamp(healthBarAmount, 0, 1);
                healthCounter.text = TargetHealth * 100 + "%";
                healthBar.color = colorGradient.Evaluate(TargetHealth);
        }
    }
}
