using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ChoseGameField))]
public class LevelsShop : MonoBehaviour
{
    [SerializeField] private StartScreenScoreCounter _scoreCounter;
    [SerializeField] private ConfirmingBuyPanel _confirmingBuyPanel;
    [SerializeField] private RewardedVideoAds _rewarded;
    [SerializeField] private int[] _levelsOpeningWithAds;

    private GameField _buyableGameField;
    public void OpenConfirmedBuyPanel(GameField gameField)
    {
        _buyableGameField = gameField;
        _confirmingBuyPanel.ShowCoast(_buyableGameField.Price);
    }

    public void Cancel()
    {
        _confirmingBuyPanel.gameObject.SetActive(false);
    }
    public void Buy()
    {
        if (_buyableGameField.OpenLevel())
        {
            if (_levelsOpeningWithAds.Contains(_buyableGameField.LevelIndex))
            {
                _rewarded.UnityAdsDidFinish += ConfirmPurchase;
                _rewarded.UnityAdsDidFinish += _buyableGameField.GameFieldOpenning;
                _rewarded.ShowRewardedVideo(false);
            }
            else
            {
                _buyableGameField.GameFieldOpenning();
                ConfirmPurchase();
            }

            Cancel();
        }
        else
        {
            _confirmingBuyPanel.PlayDangeringAnimation();
        }
    }

    private void ConfirmPurchase()
    {
        _rewarded.UnityAdsDidFinish -= ConfirmPurchase;
        _rewarded.UnityAdsDidFinish -= _buyableGameField.GameFieldOpenning;
        _scoreCounter.ReductionScore((int)_buyableGameField.Price);
    }
}
