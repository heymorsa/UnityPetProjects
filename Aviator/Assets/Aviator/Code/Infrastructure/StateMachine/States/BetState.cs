using Aviator.Code.Core.Timer;
using Aviator.Code.Core.UI;
using Aviator.Code.Core.UI.Gameplay.BetPanel;
using Aviator.Code.Infrastructure.StateMachine.StateSwitcher;
using Aviator.Code.Services.EntityContainer;
using Aviator.Code.Services.UserBalance;

namespace Aviator.Code.Infrastructure.StateMachine.States
{
    public class BetState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly IUserBalance _userBalance;

        private BetPanel _betPanel;
        private BetPanelView _betPanelView;
        private ITimer _timer;

        public BetState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, IUserBalance userBalance)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _userBalance = userBalance;
        }
        
        public void Enter()
        {
            SetDependencies();
            StartBetTime();
            TryResetBalance();
        }

        public void Exit() => _timer.OnExpired -= StartMultiplierRun;

        private void StartBetTime()
        {
            _entityContainer.GetEntity<FieldText>().ResetColor();
            _betPanelView.SetBetActive(true);
            _betPanelView.SetCashOutActive(false);
            _betPanelView.HideUserBet();
            
            _timer.OnExpired += StartMultiplierRun;
            _timer.Start();
        }

        private void StartMultiplierRun()
        {
            double userBet = _betPanel.GetBet();
            double autoCashOutMultiplier = GetAutoCashOutMultiplier();
            _userBalance.Minus(userBet);
            _entityContainer.GetEntity<BetPanelView>().SetBetActive(false);
            _betPanel.UpdateUserBalance();
            var runData = new MultiplierRunData
            {
                UserBet = userBet,
                AutoCashOutMultiplier = autoCashOutMultiplier
            };
            _stateSwitcher.SwitchTo<MultiplierRunState>(runData);
        }
        
        private float GetAutoCashOutMultiplier()
        {
            // Здесь предполагается, что у вас есть доступ к соответствующему InputField через _betPanelView
            if (float.TryParse(_betPanelView.GetAutoCashOutMultiplierValue(), out float multiplier) && multiplier > 0)
            {
                return multiplier;
            }
            return 0f; // Вернуть 0, если значение невалидно или меньше нуля
        }

        private void TryResetBalance()
        {
            if (!_userBalance.TryReset()) return;
            _betPanelView.ShowResetBalanceView();
            _betPanel.UpdateUserBalance();
        }

        private void SetDependencies()
        {
            _betPanel = _entityContainer.GetEntity<BetPanel>();
            _betPanelView = _entityContainer.GetEntity<BetPanelView>();
            _timer = _entityContainer.GetEntity<ITimer>();
        }
    }
    public class MultiplierRunData
    {
        public double UserBet { get; set; }
        public double AutoCashOutMultiplier { get; set; }
    }
}