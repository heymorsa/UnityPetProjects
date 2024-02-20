using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CarAttack : MonoBehaviour
{
    public int health = 100;
    public float radius = 5f;
    public GameObject bullet;
    private Coroutine coroutine = null;

    void Update()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        if (hitColliders.Length == 0 && coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;

            if (gameObject.CompareTag("Enemy"))
            {
                GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
            }
        }


        foreach (var el in hitColliders)
        {
            if ((gameObject.CompareTag("Player") && el.gameObject.CompareTag("Enemy")) ||
                ((gameObject.CompareTag("Enemy") && el.gameObject.CompareTag("Player"))))
            {
                if (gameObject.CompareTag("Enemy"))
                    GetComponent<NavMeshAgent>().SetDestination(el.transform.position);

                if (coroutine == null)
                    coroutine = StartCoroutine(StartAttack(el));
            }

        }
    }

    IEnumerator StartAttack(Collider enemyPos)
    {
        GameObject obj = Instantiate(bullet, transform.GetChild(1).position, Quaternion.identity);
        obj.GetComponent<BulletSpawner>().position = enemyPos.transform.position;
        yield return new WaitForSeconds(1f);
        StopCoroutine(coroutine);
        coroutine = null;
    }
}
