using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonUI : MonoBehaviour
{
    private Image cooldownBar;
    private Button spawnButton;

    private float elapsed;    
    private float duration;     

    private void Start()
    {
        spawnButton = GetComponent<Button>();

        var images = GetComponentsInChildren<Image>(true);
        foreach (var img in images)
        {
            if (img.gameObject != gameObject)
            {
                cooldownBar = img;
                break;
            }
        }

        spawnButton.onClick.AddListener(() =>
        {
            ObjectSpawner.Instance.Spawn(GameDatabase.Instance.currentProduct);
        });
    }

    private void OnEnable()
    {
        ObjectSpawner.OnSpawnStateChanged += HandleSpawnStateChanged;
    }

    private void OnDisable()
    {
        ObjectSpawner.OnSpawnStateChanged -= HandleSpawnStateChanged;
    }

    private void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cooldownBar.fillAmount = elapsed / duration;
        }
    }

    private void HandleSpawnStateChanged(bool canSpawn, float cooldown)
    {
        spawnButton.interactable = canSpawn;

        if (!canSpawn)
        {
            elapsed = 0f;
            duration = cooldown;
            cooldownBar.fillAmount = 0f;
        }
    }
}