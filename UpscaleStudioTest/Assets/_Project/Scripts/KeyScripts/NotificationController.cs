using UnityEngine;

public class NotificationController : MonoBehaviour, IWallObserver
{
    public GameObject notificationUI;

    private void Start()
    {
        Key.RegisterWallObserver(this);
        if (notificationUI != null)
        {
            notificationUI.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Key.UnregisterWallObserver(this);
    }

    public void OnAllKeysCollected()
    {
        if (notificationUI != null)
        {
            notificationUI.SetActive(true);
        }
    }
}