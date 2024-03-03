using System.Collections;
using TMPro;
using UnityEngine;
using UI.InGame;
using Game.Tile;
using Game.EnvironmentGen;
using UnityEngine.SceneManagement;
namespace Game {
    public class InGameManager :GameManager {
        public TileBoard board;
        private int countState = 0;
        public TileInfo tileInfo;
        private int score;
        [SerializeField] InGameUIManager inGameUIManager;
        [SerializeField] Environment environment;

        private void Awake() {
            Init();
        }

        private void Init() {
            NewGame();
            inGameUIManager.Init();
            environment.Init();
            tileInfo.Init();
        }

        public void NewGame() {
            // reset score
            UnfreezeGame();
            SetScore(0);

            inGameUIManager.ClosePauseMenu();
            board.ClearBoard();
            board.CreateTile();
            board.enabled = true;
            environment.NewGame();
        }

        public void GameOver() {
            board.enabled = false;
            inGameUIManager.GameOver(score);
            FreezeGame();
        }

        public void IncreaseScore(int points) {
            SetScore(score + points);
        }

        private void SetScore(int score) {
            this.score = score;
            inGameUIManager.IncreaseScore(score);
            SaveHiscore();
        }

        private void SaveHiscore() {
            int hiscore = LoadHiscore();

            if (score > hiscore) {
                PlayerPrefs.SetInt("2048score", score);
                inGameUIManager.IncreaseRecordScore(score);
            }
        }

        private int LoadHiscore() {
            return PlayerPrefs.GetInt("2048score", 0);
        }

        public int GetScore() {
            return (score);
        }

        public void SetCountState(int state) {
            if (state > countState) {
                countState = state;
                tileInfo.OpenInfo(state);
            }
        }
    }
}