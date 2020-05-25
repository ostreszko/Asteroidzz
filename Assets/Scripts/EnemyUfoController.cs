using System.Collections;
using UnityEngine;

//Enemy ufo that shows sometimes controller
public class EnemyUfoController : MonoBehaviour
{

    public Rigidbody2D rb;
    LocalGameMaster lgm;
    ObjectPooler objectPooler;
    void Start()
    {
        lgm = LocalGameMaster.LGM;
        objectPooler = ObjectPooler.Instance;
        rb.velocity = new Vector2(lgm.rand.Next(-70, 70), 0f);
    }
    private void OnEnable()
    {
        StartCoroutine(ShootRoutine()); 
    }

    IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(3);
        Transform projectileTransform = objectPooler.SpawnFromPool("EnemyProjectile", transform.position, transform.rotation).GetComponent<Transform>();
        projectileTransform.rotation = Quaternion.Euler(new Vector3(0, 0, CalculateShootAngle(projectileTransform.position, lgm.playerTransform.position)));

        StartCoroutine(ShootRoutine());
    }

    private float CalculateShootAngle(Vector3 objectPosition, Vector3 target)
    {
        target.z = 0f;
        target.x = target.x - objectPosition.x;
        target.y = target.y - objectPosition.y;
        return Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            lgm.enemiesLeft--;
            lgm.CheckEnemiesLeft();
            objectPooler.SpawnFromPool("ShipExplosion", transform.position, transform.rotation);
            lgm.audioManager.Play("ExplosionShip");
            GUIEvents.guiEvents.OnScore(200);
            gameObject.SetActive(false);
        }
    }
}
