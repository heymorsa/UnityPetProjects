using System.Collections;
using UnityEngine;

public class SpawnCarScript : MonoBehaviour
{
    public bool isEnemy = false;
    public GameObject car;
    public float time = 5f;
    void Start()
    {
        StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar()
    {
        for (int i = 1; i <= 3; i++)
        {
            yield return new WaitForSeconds(time);
            Vector3 pos = new Vector3(
                transform.GetChild(0).position.x + UnityEngine.Random.Range(3f, 7f),
                transform.GetChild(0).position.y,
                transform.GetChild(0).position.z + UnityEngine.Random.Range(3f, 7f)
                );
            GameObject spawn = Instantiate(car, pos, Quaternion.identity);
            if (isEnemy)
                spawn.tag = "Enemy";
        }
    }

}
