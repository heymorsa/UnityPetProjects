using System;
using Shapes;
using UnityEngine;
using Object = UnityEngine.Object;

public class Rectangle : IShape
{
  public event Action<IShape> Destroyed;

  private GameObject cubePrefab;

  public Rectangle(GameObject prefab)
  {
    cubePrefab = prefab;
  }


  public void SpawnAtPosition(Vector3 position)
  {
    GameObject shapeObject = Object.Instantiate(cubePrefab, position, Quaternion.identity);
    CheckForDestroy checkForDestroy = shapeObject.GetComponent<CheckForDestroy>();
    checkForDestroy.Destroyed += () => Destroyed?.Invoke(this);
    
    Vector3 newScale = shapeObject.transform.localScale;
    newScale.x *= 2.0f;
    newScale.z = 0.01f;
    shapeObject.transform.localScale = newScale;
    
    MeshCollider meshCollider = shapeObject.AddComponent<MeshCollider>();
    meshCollider.convex = true;
  }



  public void SetColor(Color color)
  {
    cubePrefab.GetComponent<Renderer>().sharedMaterial.color = color;
  }
}