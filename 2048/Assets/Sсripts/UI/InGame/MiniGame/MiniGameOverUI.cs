using UnityEngine.UI;
using UnityEngine;
using UI.Common;
using TMPro;
using UnityEngine.SceneManagement;
namespace UI.InGame {
    public class MiniGameOverUI :MenuWindow {
        [SerializeField] InGameUIMiniGameManager inGameUIManager;

        [SerializeField] TMP_Text currScoreText;
        [SerializeField] TMP_Text recordScoreText;

        [SerializeField] Button menuButton;
        [SerializeField] Button restartGameButton;
        [SerializeField] Button nextLevelGameButton;
        public override void Init(bool isOpen = false) {
            base.Init(isOpen);
            restartGameButton.onClick.AddListener(RestartLevel);
            nextLevelGameButton.onClick.AddListener(SecretLevel);
        }

        public void SetCurrScore(int value) => currScoreText.text = "SCORE = " + value.ToString();
        public void SetRecordScore() => recordScoreText.text = PlayerPrefs.GetInt("FindTwinsScore").ToString();
        private void RestartLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //inGameUIManager.inGameManager.NewGame();
        }

        private void SecretLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

    }
}