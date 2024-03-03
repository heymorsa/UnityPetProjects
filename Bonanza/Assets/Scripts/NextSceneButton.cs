using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButton : MonoBehaviour
{
  public string nextSceneName;

  public void LoadNextSceneWithDelay()
  {
    StartCoroutine(LoadNextSceneAfterDelay());
  }

  IEnumerator LoadNextSceneAfterDelay()
  {
    // Ждем 2 секунды
    yield return new WaitForSeconds(2f);

    // Загружаем следующую сцену
    SceneManager.LoadScene(nextSceneName);
  }
}