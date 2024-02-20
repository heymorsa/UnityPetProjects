using System.Collections;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] float maxPos;
    [SerializeField] float SpawnDelay;
    public GameObject Star;
    public static StarSpawner Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        StartSpawning();
        Spawner();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Spawner()
    {
        float randomSpawn = Random.Range(-maxPos, maxPos);
        Vector2 randomPos = new Vector2(randomSpawn, transform.position.y);
        Instantiate(Star, randomPos, transform.rotation);
    }
    IEnumerator SpawnStars()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            Spawner();
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
    public void StartSpawning()
    {
        StartCoroutine("SpawnStars");
    }
    public void StopSpawning()
    {
        StopCoroutine("SpawnStars");
    }
}
