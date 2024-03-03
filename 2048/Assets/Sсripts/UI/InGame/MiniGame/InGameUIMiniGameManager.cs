using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;
namespace UI.InGame {
    public class InGameUIMiniGameManager :MonoBehaviour {
        [SerializeField] MiniGameOverUI gameOverUI;
        [SerializeField] MiniGameUI gameUI;
        [SerializeField] MiniPauseMenu pauseMenuUI;

        public void Init() {
            gameOverUI.Init(false);
            pauseMenuUI.Init(false);
            gameUI.Init(true);
        }

        public void OpenPauseMenu() {
            pauseMenuUI.Open();
            gameUI.Close();
        }
        public void ClosePauseMenu() {
            pauseMenuUI.Close();
            gameUI.Open();
            gameOverUI.Close();
        }

        public void GameOver(int score) {
           
            pauseMenuUI.Close();
            gameUI.Close();
            gameOverUI.Open();
            gameOverUI.SetCurrScore(score);
            gameOverUI.SetRecordScore();
        }

        public void IncreaseScore(int value) => gameUI.ChangeScore(value);
        public void IncreaseRecordScore(int value) => gameUI.ChangeRecordScore(value);
        
    }
}