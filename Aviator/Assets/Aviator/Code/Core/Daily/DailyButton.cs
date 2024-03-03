using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;
using Zenject;

namespace Aviator.Code.Core.Daily
{
    public class DailyButton : MonoBehaviour
    {
        [SerializeField]
        public Button button;
        [SerializeField]
        public TextMeshProUGUI countdownText;
        private Daily _daily;
        

        private bool isButtonEnabled = true;
        private DateTime nextClickTime;
        
        private const string IsButtonEnabledKey = "IsButtonEnabled";
        private const string NextClickTimeKey = "NextClickTime";
       
        [Inject]
        public void Construct(Daily daily)
        {
            _daily = daily;
        }

        private void Awake()
        {
            button.onClick.AddListener(OnButtonClick);
            LoadData();
            UpdateCountdownText();
        }

        private void Update()
        {
            if (!isButtonEnabled)
            {
                TimeSpan timeRemaining = nextClickTime - DateTime.Now;
                if (timeRemaining.TotalSeconds <= 0)
                {
                    isButtonEnabled = true;
                    button.interactable = true;
                    UpdateCountdownText();
                }
                else
                {
                    UpdateCountdownText((float)timeRemaining.TotalSeconds);
                    button.interactable = false;
                }
            }
        }

        private void OnButtonClick()
        {
            if (isButtonEnabled)
            {
                isButtonEnabled = false;
                button.interactable = false;
                nextClickTime = DateTime.Now.AddHours(12);
                SaveData();
                UpdateCountdownText();
                _daily.DailyReward();
            }
        }

        private void UpdateCountdownText(float timeRemaining = 0)
        {
            if (isButtonEnabled)
            {
                countdownText.text = "";
            }
            else
            {
                string formattedTime = FormatTime(timeRemaining);
                countdownText.text = " " + formattedTime;
            }
        }

        private string FormatTime(float seconds)
        {
            int hours = Mathf.FloorToInt(seconds / 3600);
            int minutes = Mathf.FloorToInt((seconds % 3600) / 60);
            int secondsInt = Mathf.FloorToInt(seconds % 60);

            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, secondsInt);
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt(IsButtonEnabledKey, isButtonEnabled ? 1 : 0);
            PlayerPrefs.SetString(NextClickTimeKey, nextClickTime.ToString("o"));
            PlayerPrefs.Save();
        }

        private void LoadData()
        {
            isButtonEnabled = PlayerPrefs.GetInt(IsButtonEnabledKey, 1) == 1;
            string nextClickTimeStr = PlayerPrefs.GetString(NextClickTimeKey, DateTime.Now.ToString("o"));
            if (DateTime.TryParse(nextClickTimeStr, out DateTime parsedTime))
            {
                nextClickTime = parsedTime;
            }
        }
    }
}