using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public Vector2 safeGroundLocation { get; private set; } = Vector2.zero;
    [SerializeField] private LayerMask whatIsCheckPoint;

    public ParticleSystem particleSystemsKO;

    public GameObject RespawnAura;
    public GameObject CarSprites;
    public GameObject Explosion;
    public GameObject DamageAnim;
    private CarSFXHandler carSfxHandler;
    private HealthSystem healthSystem;

    private void Awake()
    {
        carSfxHandler = GetComponent<CarSFXHandler>();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        safeGroundLocation = transform.position;
    }

    private void Update()
    {
        if (healthSystem.health <= 0)
        {
            //Destroy(gameObject);
            StartCoroutine(Respawn());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatIsCheckPoint.value & (1 << collision.gameObject.layer)) > 0)
        {
            safeGroundLocation = collision.bounds.center;
            //PlayHitEffect();
        }
    }

    public void PlayHitEffect()
    {
        DamageAnim.SetActive(true); // Play the particle system when the player is hit
    }

    private IEnumerator Respawn()
    {
        yield return StartCoroutine(DestroyAnimation());
        Invoke("PlayExplosionSfx", 0.1f); // Schedule the sound effect to play after a small delay
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed
        yield return StartCoroutine(BetweenDestroyAndRespawn());
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(RespawnAnimation());
    }

    private IEnumerator DestroyAnimation()
    {
        Explosion.SetActive(true);
        CarSprites.SetActive(false);
        particleSystemsKO.Play();
        yield return null;
    }

    private IEnumerator BetweenDestroyAndRespawn()
    {
        transform.position = safeGroundLocation;
        Explosion.SetActive(false);
        RespawnAura.SetActive(true);
        yield return null;
    }

    private IEnumerator RespawnAnimation()
    {
        healthSystem.RecoverHealth();
        RespawnAura.SetActive(false);
        CarSprites.SetActive(true);
        yield return null;
    }

    private void PlayExplosionSfx()
    {
        carSfxHandler.PlayExplosionSfx(); // Play explosion sound effect
    }
}