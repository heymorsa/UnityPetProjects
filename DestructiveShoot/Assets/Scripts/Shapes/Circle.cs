using System;
using Shapes;
using UnityEngine;
using Object = UnityEngine.Object;

public class Circle : IShape
{
  public event Action<IShape> Destroyed;
  private GameObject spherePrefab;

  public Circle(GameObject prefab)
  {
    spherePrefab = prefab;
  }
  
  public void SpawnAtPosition(Vector3 position)
  {
    GameObject shapeObject = Object.Instantiate(spherePrefab);
    shapeObject.transform.position = position;
    CheckForDestroy checkForDestroy = shapeObject.GetComponent<CheckForDestroy>();
    checkForDestroy.Destroyed += () => Destroyed?.Invoke(this);

    Vector3 currentScale = shapeObject.transform.localScale;
    
    Vector3 newScale = new Vector3(currentScale.x, currentScale.y, 0.01f);
    
    shapeObject.transform.localScale = newScale;

    MeshCollider meshCollider = shapeObject.AddComponent<MeshCollider>();
    meshCollider.convex = true;
  }

  public void SetColor(Color color)
  {
    spherePrefab.GetComponent<Renderer>().sharedMaterial.color = color;
  }

}