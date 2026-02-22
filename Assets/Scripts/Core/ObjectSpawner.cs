using System;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{ 
    public static ObjectSpawner Instance { get; private set; }
    public SpawnerData Data { get; private set; }

    public static event Action<bool, float> OnSpawnStateChanged;

    bool canSpawn = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Data = GameDatabase.Instance.SpawnerData;
    }

    public void Spawn(ProductData d)
    {
        if (!canSpawn)
        {
            Debug.LogWarning("Cannot spawn object. Spawning is currently disabled.");
            return;
        }

        GameObject prefab = GameDatabase.Instance.productPrefab;
        if (prefab == null)
        {
            Debug.LogError("Prefab is null. Cannot spawn object.");
            return;
        }

        Transform spawnPoint = GameDatabase.Instance.productSpawnPoint;
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not set in GameDatabase.");
            return;
        }

        for (int i = 0; i < Data.spawnCount; i++)
        {
            GameObject p = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            p.GetComponent<ProductDataHolder>().Initialize(d);
        }

        StartCoroutine(SpawnCooldown());
    }

    private System.Collections.IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        OnSpawnStateChanged?.Invoke(canSpawn, Data.spawnCooldown);
        yield return new WaitForSeconds(Data.spawnCooldown);
        canSpawn = true;
        OnSpawnStateChanged?.Invoke(canSpawn, Data.spawnCooldown);
    }
}