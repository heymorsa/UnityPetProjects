using System;
using UnityEngine;

public class CheckForDestroy : MonoBehaviour
{
  public event Action Destroyed;

  private void OnDestroy()
  {
    Destroyed?.Invoke();
  }
}