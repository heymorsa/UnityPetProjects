using System.Collections.Generic;
using UnityEngine;

public class FirstPersonShooter : MonoBehaviour
{
  public Transform bulletSpawnPoint;
  public GameObject bulletPrefab;

  public float shootForce = 10f;
  public float fireRate = 0.2f;
  private float fireTimer = 0f;

  private int currentColorIndex = 0;
  private List<Color> bulletColors = new List<Color>();

  private void Start()
  {
    bulletColors = JsonColorProvider.LoadColorsFromJson();
  }

  public void Shoot()
  {
    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

    Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
    Renderer bulletRenderer = bullet.GetComponent<Renderer>();

    if (bulletColors.Count > 0 && bulletRenderer)
    {
      bulletRenderer.material.color = bulletColors[currentColorIndex];
      currentColorIndex = (currentColorIndex + 1) % bulletColors.Count;
    }

    if (bulletRigidbody)
    {
      bulletRigidbody.velocity = bulletSpawnPoint.forward * shootForce;
    }

    Destroy(bullet, 5f);
  }
}