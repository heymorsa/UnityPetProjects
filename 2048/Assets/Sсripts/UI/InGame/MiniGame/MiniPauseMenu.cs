using UnityEngine.UI;
using UnityEngine;
using UI.Common;
using TMPro;
namespace UI.InGame {
    public class MiniPauseMenu :MenuWindow {
        [SerializeField] InGameUIMiniGameManager inGameUIMiniGameManager;
        [SerializeField] Button continueGameButton;
        [SerializeField] Button menuButton;
        [SerializeField] Button restartGameButton;

        public override void Init(bool isOpen = false) {
            base.Init(isOpen);
            continueGameButton.onClick.AddListener(ContinueGame);
        }
        private void ContinueGame() {
            inGameUIMiniGameManager.ClosePauseMenu();
        }
        
    }
}