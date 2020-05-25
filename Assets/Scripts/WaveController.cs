using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    LocalGameMaster lgm;
    ObjectPooler objectPooler;
    System.Random rand = new System.Random();
    float safeAsteroidSpawnDistance = 30f;
    [System.NonSerialized]
    public int waveNumber = 0;
    float ufoSpawnTime = 15f;
    float remaminigTimeToSpawnUfo;
    public Text waveNumberText;

    
    void Start()
    {
        lgm = LocalGameMaster.LGM;
        objectPooler = ObjectPooler.Instance;
        remaminigTimeToSpawnUfo = ufoSpawnTime;
        GUIEvents.guiEvents.onWaveIncrementTrigger += IncrementWave;

    }

    // Update is called once per frame
    void Update()
    {
        if (lgm.enemiesLeft == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            GUIEvents.guiEvents.onWaveIncrement();
            SpawnWave();
            lgm.infoLabelGameObject.SetActive(false);
            lgm.battleInProgress = true;
        }
        if (lgm.battleInProgress)
        {
            remaminigTimeToSpawnUfo -= Time.deltaTime;
            if (remaminigTimeToSpawnUfo <= 0)
            {
                remaminigTimeToSpawnUfo = ufoSpawnTime;
                lgm.enemiesLeft++;
                objectPooler.SpawnFromPool("EnemyUfo", SpawnEnemyObjectInSafePosition(), new Quaternion(0, 0, 0, 0));
            }
        }
    }

    void SpawnWave()
    {
        int asteroidsCount = (waveNumber * 2) + 6;
        for (int i = 0; i < asteroidsCount; i++)
        {
            GameObject spawnedAsteroid = objectPooler.SpawnFromPool("BigAsteroid", SpawnEnemyObjectInSafePosition(), new Quaternion(0, 0, 0, 0));
            AsteroidsHelper.GenerateRandomVelocity(spawnedAsteroid);
            lgm.enemiesLeft++;
        }
    }
    Vector2 SpawnEnemyObjectInSafePosition()
    {
        Vector2 safePosition;
        do
        {
            safePosition = new Vector2(rand.Next((int)lgm.leftX, (int)lgm.rightX), rand.Next((int)lgm.bottomY, (int)lgm.topY));
        } while (Vector2.Distance(safePosition, lgm.playerTransform.position) < safeAsteroidSpawnDistance);
        return safePosition;
    }

    private void IncrementWave()
    {
        waveNumber++;
        waveNumberText.text = waveNumber.ToString();
    }

}
