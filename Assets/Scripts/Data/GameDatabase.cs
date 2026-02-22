using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDatabase : MonoBehaviour
{
    public static GameDatabase Instance { get; private set; }

    [Header("Game Data")]
    [SerializeField] public SpawnerData SpawnerData;
    [SerializeField] public ConveyorData ConveyorData;
    [SerializeField] public ScorerData ScorerData;
    [SerializeField] public ProductData[] Products;

    [Header("Scene Data")]
    [SerializeField] public Transform productSpawnPoint;
    [SerializeField] public GameObject productPrefab;

    public ProductData currentProduct { get; private set; }
    private int currentProductIndex = 0;

    private Dictionary<string, ProductData> productMap;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        productMap = Products.ToDictionary(p => p.productName); 

        currentProduct = GetProductByIndex(currentProductIndex);
    }

    public void UpgradeCurrentProduct()
    {
        currentProductIndex++;
        if (currentProductIndex >= Products.Length)
        {
            Debug.Log("All products unlocked!");
            currentProductIndex = Products.Length - 1; 
        }
        currentProduct = GetProductByIndex(currentProductIndex);
    }

    public ProductData GetProductByIndex(int index)
    {
        if (index < 0 || index >= Products.Length)
        {
            Debug.LogError("Invalid product index!");
            return null;
        }

        return Products[index];
    }
    public ProductData GetProductByName(string name)
    {
        productMap.TryGetValue(name, out var product);
        return product;
    }
}
