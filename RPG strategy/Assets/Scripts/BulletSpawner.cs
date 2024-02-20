using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Vector3 position;
    public float bulletSpeed = 30f;
    void Update()
    {
        float step = bulletSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
        if (transform.position == position)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            CarAttack attack = other.GetComponent<CarAttack>();
            attack.health -= 20;

            Transform healthBar = other.transform.GetChild(0).transform;
            healthBar.localScale = new Vector3(
                healthBar.localScale.x - 0.3f,
                healthBar.localScale.y,
                healthBar.localScale.z
                );
            if (attack.health <= 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
