using UnityEngine;

public class WallController : MonoBehaviour, IWallObserver
{
    public GameObject wall;

    private void Start()
    {
        Key.RegisterWallObserver(this);
    }

    private void OnDestroy()
    {
        Key.UnregisterWallObserver(this);
    }

    public void OnAllKeysCollected()
    {
        if (wall != null)
        {
            wall.SetActive(false);
        }
    }
}