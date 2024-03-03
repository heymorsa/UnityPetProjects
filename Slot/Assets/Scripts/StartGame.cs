using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
  public Button yourButton;
  public GameObject animationObject;
  public Animator animator;
  public string sceneToLoadAfterAnimation;

  private void Start()
  {
    yourButton.onClick.AddListener(HandleButtonClick);
  }

  void HandleButtonClick()
  {
    yourButton.gameObject.SetActive(false);
    
    animationObject.SetActive(true);
    
    animator.Play("PlayerAnimation");
    
    StartCoroutine(WaitForAnimation());
  }

  System.Collections.IEnumerator WaitForAnimation()
  {
    yield return new WaitForSeconds(2.0f);
    
    SceneManager.LoadScene(sceneToLoadAfterAnimation);
  }
}