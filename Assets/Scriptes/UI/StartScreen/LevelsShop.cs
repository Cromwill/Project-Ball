using UnityEngine;

[RequireComponent(typeof(ChoseGameField))]
public class LevelsShop : MonoBehaviour
{
    [SerializeField] private StartScreenScoreCounter _scoreCounter;
    [SerializeField] private ConfirmingBuyPanel _confirmingBuyPanel;

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
            _scoreCounter.ReductionScore((int)_buyableGameField.Price);
            Cancel();
        }
        else
        {
            _confirmingBuyPanel.PlayDangeringAnimation();
        }
    }
}
