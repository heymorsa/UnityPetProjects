using UnityEngine.UI;
using UnityEngine;
using UI.Common;
using TMPro;
namespace UI.InGame {
    public class PauseMenu :MenuWindow {
        [SerializeField] InGameUIManager inGameUIManager;
        [SerializeField] Button continueGameButton;
        [SerializeField] Button menuButton;
        [SerializeField] Button restartGameButton;

        public override void Init(bool isOpen = false) {
            base.Init(isOpen);
            continueGameButton.onClick.AddListener(ContinueGame);
            menuButton.onClick.AddListener(CloseLevel);
            restartGameButton.onClick.AddListener(RestartLevel);
        }
        private void ContinueGame() {
            inGameUIManager.ClosePauseMenu();
            inGameUIManager.UnfreezeGame();
        }
        private void RestartLevel() {
        inGameUIManager.UnfreezeGame();
            inGameUIManager.inGameManager.NewGame();
        }
        private void CloseLevel() {
        inGameUIManager.UnfreezeGame();
            inGameUIManager.inGameManager.CloseGame();
        }
    }
}