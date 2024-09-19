using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
    public GameObject bulletPrefab;

    private void Start()
    {
        ScheduleNextShot();
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        ScheduleNextShot();
    }

    private void ScheduleNextShot()
    {
        float randomInterval = Random.Range(2.5f, 3.5f);
        Invoke(nameof(Shoot), randomInterval);
    }
}