using System.Collections;
using Aviator.Code.Core.MultiplierRunner;
using Aviator.Code.Core.Plane;
using Aviator.Code.Core.UI;
using Aviator.Code.Core.UI.Gameplay.BetPanel;
using Aviator.Code.Core.UI.Gameplay.TopPanel;
using Aviator.Code.Core.UI.Statistics;
using Aviator.Code.Data.Enums;
using Aviator.Code.Infrastructure.StateMachine.StateSwitcher;
using Aviator.Code.Services;
using Aviator.Code.Services.EntityContainer;
using Aviator.Code.Services.Sound;
using Aviator.Code.Services.UserBalance;
using UnityEngine;

namespace Aviator.Code.Infrastructure.StateMachine.States
{
    public class MultiplierRunState : IPayloadedState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly IUserBalance _userBalance;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISoundService _soundService;

        private IMultiplierRunner _multiplierRunner;
        private PlaneView _planeView;
        private BetPanelView _betPanelView;
        private StatisticsScreen _statisticsScreen;
        private double _userBet;
        private bool _isCashOut;
        private float _autoCashOutMultiplier = 0f;
        

        public MultiplierRunState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, 
            IUserBalance userBalance, ICoroutineRunner coroutineRunner, ISoundService soundService)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _userBalance = userBalance;
            _coroutineRunner = coroutineRunner;
            _soundService = soundService;
        }
        
        public void Enter(object payload)
        {
            var runData = payload as MultiplierRunData;
            if (runData != null)
            {
                _userBet = runData.UserBet;
                _autoCashOutMultiplier = (float)runData.AutoCashOutMultiplier;
                _isCashOut = false;
                SetDependencies();
                StartRun();
                if (_autoCashOutMultiplier > 0)
                {
                    _coroutineRunner.StartCoroutine(AutoCashOutCoroutine());
                }
            }
            else
            {
                // Обработка ошибки или возврат, если payload не является MultiplierRunData
            }
        }

        public void Exit()
        {
            _multiplierRunner.OnReached -= OnRunFinished;
            _betPanelView.OnCashOutClick -= OnUserCashOut;
        }

        private void SetDependencies()
        {
            _multiplierRunner = _entityContainer.GetEntity<IMultiplierRunner>();
            _multiplierRunner.OnReached += OnRunFinished;
            _betPanelView = _entityContainer.GetEntity<BetPanelView>();
            _betPanelView.OnCashOutClick += OnUserCashOut;
            _planeView = _entityContainer.GetEntity<PlaneView>();
            _statisticsScreen = _entityContainer.GetEntity<StatisticsScreen>();
        }

        private void StartRun()
        {
            _multiplierRunner.RunToMultiplier();
            _betPanelView.SetCashOutActive(_userBet != 0);
            _planeView.StartFly();
        }
       
        public void SetAutoCashOutMultiplier(float multiplier)
        {
            _autoCashOutMultiplier = multiplier;
        }
        
        private IEnumerator AutoCashOutCoroutine()
        {
            while (!_isCashOut && _userBet != 0)
            {
                if (_multiplierRunner.GetMultiplier() >= _autoCashOutMultiplier)
                {
                    OnUserCashOut();
                    break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        private void OnUserCashOut()
        {
            double userWin = DefineUserWin(out float multiplier);
            _betPanelView.SetCashOutActive(false);
            _entityContainer.GetEntity<BetPanel>().UpdateUserBalance();
            _entityContainer.GetEntity<WinPopUp>().Show(userWin);
            _statisticsScreen.AddStatistic(_userBet, multiplier, userWin - _userBet);
            _soundService.PlayEffectSound(SoundId.Win);
           // _coroutineRunner.StopCoroutine(AutoCashOutCoroutine());
        }


        private double DefineUserWin(out float multiplier)
        {
            multiplier = _multiplierRunner.GetMultiplier();
            double userWin = _userBet * multiplier;
            _isCashOut = true;
            _userBalance.Add(userWin);
            return userWin;
        }

        private void OnRunFinished()
        {
            _betPanelView.SetCashOutActive(false);
            float multiplier = _multiplierRunner.GetMultiplier();
            if (!_isCashOut && _userBet != 0)
            {
                _statisticsScreen.AddStatistic(_userBet, multiplier, -_userBet);
            }
            
            _entityContainer.GetEntity<TopPanel>().AddHistoryPoint(multiplier);
            _planeView.Explode();
            _coroutineRunner.StartCoroutine(StartNewGameWithDelay());
        }

        private IEnumerator StartNewGameWithDelay()
        {
            yield return new WaitForSeconds(3.5f);
            _planeView.ResetView();
            _stateSwitcher.SwitchTo<BetState>();
        }
    }
}