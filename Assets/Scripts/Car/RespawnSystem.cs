using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public Vector2 safeGroundLocation {get; private set;} = Vector2.zero;
    [SerializeField] private LayerMask whatIsCheckPoint;
    
    public ParticleSystem particleSystemsKO;

    public GameObject RespawnAura;
    public GameObject CarSprites;
    public GameObject Explosion;

    CarSFXHandler carSfxHandler;
    HealthSystem HealthSystem;

    void Awake()
    {
        carSfxHandler = GetComponent<CarSFXHandler>();
        HealthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        safeGroundLocation = transform.position;
        //shield = transform.Find("Shield").gameObject;
        //DeactivateShield();
    }

    void Update()
    {
        if (gameObject.GetComponent<HealthSystem>().health <= 0)
        {
            StartCoroutine(Respawn());
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

    IEnumerator Respawn()
    {
        //Debug.Log("Lol");
        //health = maxHealth;
        WaitForSeconds waitTime = new WaitForSeconds(1.0f);
        yield return StartCoroutine(DestroyAnimation());
        yield return waitTime;
        yield return StartCoroutine(BetweenDestroyAndRespawn());
        yield return waitTime;
        yield return StartCoroutine(RespawnAnimation());
        //Destroy(gameObject);
    }

    public IEnumerator DestroyAnimation()
    {
        carSfxHandler.PlayExplosionSfx();
        Explosion.SetActive(true);
        CarSprites.SetActive(false);
        //particleSystemsKO.Play();
        yield return null;
    }

    public IEnumerator BetweenDestroyAndRespawn()
    {
        transform.position = safeGroundLocation;
        Explosion.SetActive(false);
        RespawnAura.SetActive(true);
        yield return null;
    }

    public IEnumerator RespawnAnimation()
    {
        HealthSystem.RecoverHealth();
        RespawnAura.SetActive(false);
        CarSprites.SetActive(true);
        yield return null;
    }
}
