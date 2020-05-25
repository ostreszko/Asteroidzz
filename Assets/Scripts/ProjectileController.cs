using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody2D rb;
    int projectileLifeTime = 4;
    ObjectPooler objectPooler;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectPooler = ObjectPooler.Instance;
    }

    private void OnEnable()
    {
        Invoke("DisactivateProjectile", projectileLifeTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    void FixedUpdate()
    {
        //I know this is wrong but have problems with resolving, and my time has ended :(
        rb.velocity = (Vector2)transform.right * 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("PlayerProjectile"))
        {
            if (collision.CompareTag("EnemyObject"))
            {
                objectPooler.SpawnFromPool("ProjectileExplosion", transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
        else if (gameObject.CompareTag("EnemyObject"))
        {
            if (collision.CompareTag("Player"))
            {
                objectPooler.SpawnFromPool("ProjectileExplosion", transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
        
    }

    void DisactivateProjectile()
    {
        gameObject.SetActive(false);
    }
}