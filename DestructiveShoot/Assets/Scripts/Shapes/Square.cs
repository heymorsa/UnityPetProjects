using System;
using Shapes;
using UnityEngine;
using Object = UnityEngine.Object;

public class Square : IShape
{
  public event Action<IShape> Destroyed;

  private GameObject cubePrefab;
  private IShape _shapeImplementation;

  public Square(GameObject prefab)
  {
    cubePrefab = prefab;
  }

  public void SpawnAtPosition(Vector3 position)
  {
    GameObject shapeObject = Object.Instantiate(cubePrefab, position, Quaternion.identity);
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
    cubePrefab.GetComponent<Renderer>().sharedMaterial.color = color;
  }
}