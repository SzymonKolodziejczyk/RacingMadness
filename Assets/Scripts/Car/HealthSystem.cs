using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public void RecoverHealth()
    {
        health = maxHealth;
    }
}
