using System;
using System.Collections;
using Aviator.Code.Core.UI;
using Aviator.Code.Services;
using UnityEngine;

namespace Aviator.Code.Core.Timer
{
    public class Timer : ITimer
    {
        public event Action OnExpired;
        
        private readonly FieldText _fieldText;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _time;
        
        private float _currentTime;
        private const float TickStep = 0.01f;

        public Timer(FieldText fieldText, ICoroutineRunner coroutineRunner, float time)
        {
            _fieldText = fieldText;
            _coroutineRunner = coroutineRunner;
            _time = time;
        }

        public void Start() => _coroutineRunner.StartCoroutine(PlayTimer());

        private IEnumerator PlayTimer()
        {
            _currentTime = _time;
            float elapsedTime = 0f;

            while (_currentTime > 0)
            {
                yield return null;
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= TickStep)
                {
                    _fieldText.SetText($"{_currentTime -= elapsedTime:0.00}");
                    elapsedTime = 0f;
                }
            }

            OnExpired?.Invoke();
        }
    }
}