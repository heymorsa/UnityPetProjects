using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UI.Common;
using TMPro;
namespace UI.InGame {
    public class GameUI :MenuWindow {
        [SerializeField] InGameUIManager inGameUIManager;
        [SerializeField] Button openPauseMenuButton;
        [SerializeField] TMP_Text scoreText;
        [SerializeField] TMP_Text recordScoreText;
        private int currentScore;
        public override void Init(bool isOpen = false) {
            base.Init(isOpen);
            StartScore();
            openPauseMenuButton.onClick.AddListener(inGameUIManager.OpenPauseMenu);
        }

        public void ChangeScore(int score) {
            currentScore = score;
            scoreText.text = "SCORE: " + currentScore.ToString();
        }
        public void ChangeRecordScore(int record) {
            recordScoreText.text =  record.ToString();    
        }

        private void StartScore() {
            ChangeRecordScore(PlayerPrefs.GetInt("2048score", 0));
            ChangeScore(0);
        }
        
    }
}