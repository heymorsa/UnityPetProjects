using UnityEngine;
using System.Collections.Generic;

public class Key : MonoBehaviour
{
    private static int keyCount = 0;
    private static List<IKeyObserver> keyObservers = new List<IKeyObserver>();
    private static List<IWallObserver> wallObservers = new List<IWallObserver>();
    public AudioClip keyPickupSound; 

    public static void RegisterKeyObserver(IKeyObserver observer)
    {
        if (!keyObservers.Contains(observer))
        {
            keyObservers.Add(observer);
        }
    }

    public static void UnregisterKeyObserver(IKeyObserver observer)
    {
        if (keyObservers.Contains(observer))
        {
            keyObservers.Remove(observer);
        }
    }

    public static void RegisterWallObserver(IWallObserver observer)
    {
        if (!wallObservers.Contains(observer))
        {
            wallObservers.Add(observer);
        }
    }

    public static void UnregisterWallObserver(IWallObserver observer)
    {
        if (wallObservers.Contains(observer))
        {
            wallObservers.Remove(observer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyCount++;
            NotifyKeyObservers();
            if (keyCount == 4)
            {
                NotifyWallObservers();
            }
            AudioManager.PlaySoundAtPosition(keyPickupSound, transform.position);

            Destroy(gameObject);
        }
    }

    private void NotifyKeyObservers()
    {
        foreach (var observer in keyObservers)
        {
            observer.OnKeyCollected(keyCount);
        }
    }

    private void NotifyWallObservers()
    {
        foreach (var observer in wallObservers)
        {
            observer.OnAllKeysCollected();
        }
    }
}