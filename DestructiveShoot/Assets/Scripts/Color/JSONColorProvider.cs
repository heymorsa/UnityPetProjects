using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class JsonColorProvider
{
  public static List<Color> LoadColorsFromJson()
  {
    string path = Path.Combine(Application.streamingAssetsPath, "Colors.json");

    if (File.Exists(path))
    {
      string json = File.ReadAllText(path);
      ColorList loadedColors = JsonUtility.FromJson<ColorList>(json);
      return loadedColors.colors;
    } else
    {
      Debug.LogError("Cannot find JSON file!");
      return new List<Color>();
    }
  }

  [System.Serializable]
  private class ColorList
  {
    public List<Color> colors;
  }
}