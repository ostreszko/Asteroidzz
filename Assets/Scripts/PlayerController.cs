using UnityEngine;
using static UnityEngine.ParticleSystem;

//Player controls
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 faceDirection;
    public float moveSpeed = 20f, maxSpeed, rotationSpeed = 5f;
    public float shootRecoilForce = 10f;
    ObjectPooler objectPooler;
    LocalGameMaster lgm;
    public ParticleSystem particleSmoke;
    EmissionModule smokeEmision;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lgm = LocalGameMaster.LGM;
        objectPooler = ObjectPooler.Instance;
        Time.timeScale = 1f;
        smokeEmision = particleSmoke.emission;
    }
    private void Update()
    {
        Shoot();
    }

    void FixedUpdate()
    {
        RotatePlayer();
        PlayerMove();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lgm.audioManager.Play("Shoot1");
            rb.AddForce(-faceDirection * shootRecoilForce);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            objectPooler.SpawnFromPool("PlayerProjectile",transform.position + (transform.right * 5), transform.rotation);
        }
    }

    private void RotatePlayer()
    {
        faceDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void PlayerMove()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            lgm.audioManager.PlayContinously("ShipMoveSound");
            smokeEmision.enabled = true;
            rb.AddForce(faceDirection * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"), ForceMode2D.Force);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        else
        {
            lgm.audioManager.Stop("ShipMoveSound");
            smokeEmision.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyObject"))
        {
            lgm.isPlayerDead = true;
            GUIEvents.guiEvents.onLiveDeplate();
            objectPooler.SpawnFromPool("ShipExplosion", transform.position, transform.rotation);
            lgm.audioManager.Play("ExplosionShip");
            lgm.audioManager.Stop("ShipMoveSound");
            lgm.infoLabelText.text = lgm.deadPlayerInfoLabel;
            lgm.infoLabelGameObject.SetActive(true);
            lgm.battleInProgress = false;
            gameObject.SetActive(false);
        }
    }
}
