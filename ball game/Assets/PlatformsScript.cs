using System.Collections;
using UnityEngine;

public class PlatformsScript : MonoBehaviour
{
    [SerializeField] private float maxPosX;
    public GameObject Platform;
    [SerializeField] float spawnInterval;
    public static PlatformsScript instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //spawnDelay();
        //SpawnPlatforms();
        StartPlatforms();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnPlatforms()
    {

        float randomSpawn = Random.Range(-maxPosX, maxPosX);
        Vector2 randomPos = new Vector2(randomSpawn, transform.position.y);
        Instantiate(Platform, randomPos, transform.rotation);
    }
    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(0);
        while (true)
        {
            SpawnPlatforms();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public void StartPlatforms()
    {
        StartCoroutine("spawnDelay");
    }
    public void StopPlatforms()
    {
        StopCoroutine("spawnDelay");
    }
}
