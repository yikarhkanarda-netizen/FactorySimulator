using UnityEngine;

[CreateAssetMenu(menuName = "Data/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    public float spawnCooldown;
    public float spawnCount = 1;
}
