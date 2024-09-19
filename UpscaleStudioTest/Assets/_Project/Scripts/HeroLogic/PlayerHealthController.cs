using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("Health component not found");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            health.TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            health.Heal(10);
        }
    }
}