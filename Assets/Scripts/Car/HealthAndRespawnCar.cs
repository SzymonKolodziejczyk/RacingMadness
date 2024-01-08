using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndRespawnCar : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Vector2 safeGroundLocation {get; private set;} = Vector2.zero;
    [SerializeField] private LayerMask whatIsCheckPoint;
    [SerializeField] Animator transitionAnim;

    private void Start()
    {
        maxHealth = health;
        safeGroundLocation = transform.position;
        //shield = transform.Find("Shield").gameObject;
        //DeactivateShield();
    }

    void Update()
    {
        if(health <= 0)
        {
            Respawn();
        }
    }

    //Update checkpoint position
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatIsCheckPoint.value & (1 << collision.gameObject.layer)) > 0)
        {
            safeGroundLocation = new Vector2(collision.bounds.center.x, collision.bounds.center.y);
        }
    }

    void Respawn()
    {
        Debug.Log("Lol");
        health = maxHealth;
        transform.position = safeGroundLocation;
        StartCoroutine(RespawnAnimation());
        //Destroy(gameObject);
    }

    public IEnumerator RespawnAnimation()
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        transitionAnim.SetTrigger("End");
        yield return null;
    }
}
