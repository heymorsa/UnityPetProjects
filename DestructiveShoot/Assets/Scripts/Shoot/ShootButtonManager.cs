using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public FirstPersonShooter fpsController; 
  public Button shootButton;               

  private void Start()
  {
    shootButton.onClick.AddListener(OnShootButtonClick);
  }

  private void OnShootButtonClick()
  {
    fpsController.Shoot();
  }
}