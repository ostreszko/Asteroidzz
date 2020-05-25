﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    void Start()
    {
        StartPool(ref poolDictionary, ref pools);
    }

    public void StartPool(ref Dictionary<string, Queue<GameObject>> poolDictionary, ref List<Pool> pools)
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            poolDictionary[tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        }
        else
        {
            Debug.LogWarning("object with tag " + tag + " doesn't exist");
            return null;
        }
    }

}
