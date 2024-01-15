using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public float damage;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<HealthSystem>().health > 0)
        {
            //other.gameObject.GetComponent<HealthManager>().health -= damage;
            other.gameObject.GetComponent<HealthSystem>().health -= damage;
        }
        else
        {

        }
    }
}
