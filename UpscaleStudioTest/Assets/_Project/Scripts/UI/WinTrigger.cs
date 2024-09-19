using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public GameObject winPanel;
    public Button restartButton;
    public Button mainMenuButton;
    public CameraController cameraController;

    private bool isPaused = false;

    void Start()
    {
        winPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartLevel);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PauseGame(); 
        }
    }

    void PauseGame()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0.00001f; 
        cameraController.enabled = false; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    void RestartLevel()
    {
        Time.timeScale = 1f; 
        cameraController.enabled = true; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        cameraController.enabled = true; 
        SceneManager.LoadScene("MainMenu"); 
    }
}