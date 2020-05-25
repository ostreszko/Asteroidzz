using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    ObjectPooler objectPooler;
    LocalGameMaster lgm;
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        lgm = LocalGameMaster.LGM;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject generatedAsteroid = AsteroidsHelper.GenerateSmallerAsteroid(transform, transform.GetChild(0).tag, objectPooler);
                if (generatedAsteroid != null)
                    lgm.enemiesLeft++;
            }
            lgm.enemiesLeft--;
            objectPooler.SpawnFromPool("AsteroidExplosion", transform.position, transform.rotation);
            lgm.audioManager.Play("ExplosionAsteroid");
            lgm.CheckEnemiesLeft();
            GUIEvents.guiEvents.OnScore(50);
            gameObject.SetActive(false);
        }
    }
}
