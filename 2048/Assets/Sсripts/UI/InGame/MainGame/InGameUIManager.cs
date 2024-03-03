using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;
namespace UI.InGame {
    public class InGameUIManager :MonoBehaviour {
        public InGameManager inGameManager;
        [SerializeField] GameOverUI gameOverUI;
        [SerializeField] GameUI gameUI;
        [SerializeField] PauseMenu pauseMenuUI;

        public void Init() {
            gameOverUI.Init(false);
            pauseMenuUI.Init(false);
            gameUI.Init(true);
        }

        public void OpenPauseMenu() {
            pauseMenuUI.Open();
            gameUI.Close();
            FreezeGame();
        }
        public void ClosePauseMenu() {
            pauseMenuUI.Close();
            gameUI.Open();
            gameOverUI.Close();
            UnfreezeGame();
        }

        public void GameOver(int score) {
            FreezeGame();
            pauseMenuUI.Close();
            gameUI.Close();
            gameOverUI.Open();
            gameOverUI.SetCurrScore(score);
            gameOverUI.SetRecordScore();
        }

        public void FreezeGame() => inGameManager.FreezeGame();
        public void UnfreezeGame() => inGameManager.UnfreezeGame();
        public void IncreaseScore(int value) => gameUI.ChangeScore(value);
        public void IncreaseRecordScore(int value) => gameUI.ChangeRecordScore(value);
    }
}