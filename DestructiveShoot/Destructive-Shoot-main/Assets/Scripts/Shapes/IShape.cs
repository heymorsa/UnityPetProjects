using System;
using UnityEngine;
namespace Shapes
{
  public interface IShape
  {
    public event Action<IShape> Destroyed;
    void SetColor(Color color);
    void SpawnAtPosition(Vector3 position);
  }
}