using Aviator.Code.Core.UI;
using Aviator.Code.Core.UI.Gameplay.BetPanel;
using Aviator.Code.Services.EntityContainer;
using Aviator.Code.Services.UserBalance;
using UnityEngine;
namespace Aviator.Code.Core.Daily
{
  public class Daily
  {
    private readonly IEntityContainer _entityContainer;
    private readonly IUserBalance _userBalance;
  
    public Daily(IEntityContainer entityContainer, IUserBalance userBalance)
    {
      _entityContainer = entityContainer;
      _userBalance = userBalance;
    }

    public void DailyReward()
    {
      double userWin = DefineUserReward();
      _entityContainer.GetEntity<WinPopUp>().Show(userWin);
     // var betPanel = _entityContainer.GetEntity<BetPanel>();

     // Debug.Log(betPanel==null);
    }
    private double DefineUserReward()
    {
      double userWin = Random.Range(30, 100);;
      _userBalance.Add(userWin);

      return userWin;
    }
  }
}
