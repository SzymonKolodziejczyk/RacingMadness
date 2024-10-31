using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySystem : MonoBehaviour
{
    public ParticleSystem particleSystemsKO;

    public GameObject CarSprites;
    public GameObject Explosion;

    private CarSFXHandler carSfxHandler;
    private HealthSystem healthSystem;

    private void Awake()
    {
        carSfxHandler = GetComponent<CarSFXHandler>();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if (healthSystem.health <= 0)
        {
            //Destroy(gameObject);
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        yield return StartCoroutine(DestroyAnimation());
        Invoke("PlayExplosionSfx", 0.1f); // Schedule the sound effect to play after a small delay
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed
        yield return StartCoroutine(DestroyedCar());
    }

    private IEnumerator DestroyAnimation()
    {
        Explosion.SetActive(true);
        CarSprites.SetActive(false);
        yield return null;
    }

    private IEnumerator DestroyedCar()
    {
        Explosion.SetActive(false);
        yield return null;
    }

    private void PlayExplosionSfx()
    {
        carSfxHandler.PlayExplosionSfx(); // Play explosion sound effect
    }
}