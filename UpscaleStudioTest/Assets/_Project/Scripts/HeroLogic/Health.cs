using UnityEngine;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    private int currentHealth = 100;
    private static List<IHealthObserver> observers = new List<IHealthObserver>();

    public AudioClip damageSound;

    public static void RegisterObserver(IHealthObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public static void UnregisterObserver(IHealthObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, 100);
        NotifyObservers();
        AudioManager.PlaySoundAtPosition(damageSound, transform.position);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, 100);
        NotifyObservers();
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnHealthChanged(currentHealth);
        }
    }
}