using TMPro;
using UnityEngine;

public class HealthTextUI : MonoBehaviour, IHealthObserver
{
    private TextMeshProUGUI healthText;
    private int maxHealth = 100;

    private void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        if (healthText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found");
            return;
        }

        Health.RegisterObserver(this);
        OnHealthChanged(maxHealth);
    }

    private void OnDestroy()
    {
        Health.UnregisterObserver(this);
    }

    public void OnHealthChanged(int currentHealth)
    {
        healthText.text = $"{currentHealth}/{maxHealth}";
    }
}