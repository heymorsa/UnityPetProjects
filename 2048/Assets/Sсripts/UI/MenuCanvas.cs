using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
  [SerializeField] Button startGameButton;

  private void Awake()
  {
    startGameButton.onClick.AddListener(StartGame);
  }

  private void StartGame()
  {
    Debug.Log("You clicked on start game");
    StartCoroutine(LoadNextSceneAfterDelay(6f));
  }

  private IEnumerator LoadNextSceneAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}