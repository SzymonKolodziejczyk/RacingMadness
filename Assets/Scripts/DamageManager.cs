using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public float damage;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI"))
        {
            //other.gameObject.GetComponent<HealthManager>().health -= damage;
            other.gameObject.GetComponent<HealthAndRespawnCar>().health -= damage;
        }
    }
}
