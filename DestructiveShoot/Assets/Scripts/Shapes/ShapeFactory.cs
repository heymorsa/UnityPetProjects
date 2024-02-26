using System;
using System.Collections.Generic;
using Shapes;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShapeFactory
{
  private GameObject cubePrefab;
  private GameObject spherePrefab;
  private List<Color> bulletColors = new List<Color>();

  public ShapeFactory(GameObject cubePrefab, GameObject spherePrefab,  List<Color> bulletColors)
  {
    this.cubePrefab = cubePrefab;
    this.spherePrefab = spherePrefab;
    this.bulletColors = bulletColors;
  }

  public IShape CreateRandomShape()
  {
    int shapeType = Random.Range(0, 4);

    IShape shape = shapeType switch
    {
      0 => new Circle(spherePrefab),
      1 => new Rectangle(cubePrefab),
      2 => new Pyramid(cubePrefab),
      3 => new Square(cubePrefab),
      _ => throw new ArgumentOutOfRangeException()
    };

    if (bulletColors.Count > 0)
    {
      Color randomColor = bulletColors[Random.Range(0, bulletColors.Count)];
      shape.SetColor(randomColor);
    }

    return shape;
  }
}