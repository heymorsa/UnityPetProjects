using System.Collections;
using Shapes;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
  public GameObject cubePrefab;
  public GameObject spherePrefab;
  public bool generateShapes = true;

  public void Start()
  {
    StartCoroutine(GenerateShapeCoroutine());
  }
  
  private IEnumerator GenerateShapeCoroutine()
  {
    while (true)
    {
      GenerateShapes();
      generateShapes = false;
      yield return new WaitUntil(() => generateShapes);
    }
  }
  
  public void GenerateShapes()
  {
    ShapeFactory shapeFactory = new ShapeFactory(cubePrefab, spherePrefab, JsonColorProvider.LoadColorsFromJson());
    IShape shape = shapeFactory.CreateRandomShape();
    
      Vector3 playerPosition = transform.position;
      Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), Random.Range(0f, 1f), Random.Range(-5f, 5f));
      Vector3 spawnPosition = playerPosition + randomOffset;

      shape.SpawnAtPosition(spawnPosition);
      shape.Destroyed += ShapeOnDestroyed;
  }

  private void ShapeOnDestroyed(IShape shape)
  {
    shape.Destroyed -= ShapeOnDestroyed;
    generateShapes = true;
  }
}