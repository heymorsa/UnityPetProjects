using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour, IHealthObserver
{
    private Image healthBarImage;

    private void Start()
    {
        healthBarImage = GetComponent<Image>();
        if (healthBarImage == null)
        {
            return;
        }

        Health.RegisterObserver(this);
        OnHealthChanged(100);
    }

    private void OnDestroy()
    {
        Health.UnregisterObserver(this);
    }

    public void OnHealthChanged(int currentHealth)
    {
        healthBarImage.fillAmount = currentHealth / 100f;
    }
}