using UnityEngine;

public class KeysSpawner : MonoBehaviour
{
    public GameObject keyPrefab; 
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnKeys();
    }

    void SpawnKeys()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(keyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}