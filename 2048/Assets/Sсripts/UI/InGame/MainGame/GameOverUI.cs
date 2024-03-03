using UnityEngine.UI;
using UnityEngine;
using UI.Common;
using TMPro;
using UnityEngine.SceneManagement;
namespace UI.InGame {
    public class GameOverUI :MenuWindow {
        [SerializeField] InGameUIManager inGameUIManager;

        [SerializeField] TMP_Text currScoreText;
        [SerializeField] TMP_Text recordScoreText;

        [SerializeField] Button menuButton;
        [SerializeField] Button restartGameButton;
        [SerializeField] Button nextLevelGameButton;
        public override void Init(bool isOpen = false) {
            base.Init(isOpen);
            menuButton.onClick.AddListener(CloseLevel);
            restartGameButton.onClick.AddListener(RestartLevel);
            nextLevelGameButton.onClick.AddListener(SecretLevel);
        }
        public void SetCurrScore(int value) => currScoreText.text = "SCORE = " + value.ToString();
        public void SetRecordScore() => recordScoreText.text =  PlayerPrefs.GetInt("2048score").ToString();
        private void RestartLevel() {
            inGameUIManager.inGameManager.NewGame();
        }
        private void CloseLevel() {
            inGameUIManager.inGameManager.CloseGame();
        }
        private void SecretLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}