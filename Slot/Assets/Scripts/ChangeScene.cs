using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChangeScene : MonoBehaviour
{
  // Имя сцены, на которую вы хотите перейти.
  public string sceneName;

  public void LoadScene()
  {
    SceneManager.LoadScene(sceneName);
  }

  public void QuitGame()
  {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
  }
}