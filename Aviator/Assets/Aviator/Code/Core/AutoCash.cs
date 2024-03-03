using Aviator.Code.Infrastructure.StateMachine.States;
using Aviator.Code.Infrastructure.StateMachine.StateSwitcher;
using Aviator.Code.Services.Factories.GameFactory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
// Для работы с элементами UI

namespace Aviator.Code.Core
{
  public class AutoCash : MonoBehaviour
  {
    public TMP_InputField inputField; // Поле для ввода множителя
    public Button confirmButton;  // Кнопка для подтверждения
    
    private IStateSwitcher _stateSwitcher;

    [Inject]
    private void Construct(IStateSwitcher stateSwitcher)
    {
      _stateSwitcher = stateSwitcher;
    }

    void Start()
    {
      // Настройка кнопки для вызова метода SetMultiplier
   //   confirmButton.onClick.AddListener(SetMultiplier);
    }

    private void SetMultiplier()
    {
      if (float.TryParse(inputField.text, out float multiplierValue))
      {
        // Вызов метода SetAutoCashOutMultiplier с введенным значением
       // _stateSwitcher..SetAutoCashOutMultiplier(multiplierValue);
      }
      else
      {
        Debug.LogError("Неверный формат множителя");
      }
    }

    // Метод для установки ссылки на MultiplierRunState
    public void SetMultiplierRunState(MultiplierRunState state)
    {
    //  multiplierRunState = state;
    }
  }
}