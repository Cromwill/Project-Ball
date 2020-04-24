using UnityEngine;

[RequireComponent(typeof(ChoseGameField))]
public class LevelsShop : MonoBehaviour
{
    [SerializeField] private StartScreenScoreCounter _scorreCounter;
    [SerializeField] private ConfirmingBuyPanel _confirmingBuyPanel;

    private GameField _buyableGameField;
    public void OpenConfirmedBuyPanel(GameField gameField)
    {
        _buyableGameField = gameField;
        _confirmingBuyPanel.gameObject.SetActive(true);
        _confirmingBuyPanel.ShowCoast(_buyableGameField.Price);
    }

    public void Cancel()
    {
        _confirmingBuyPanel.gameObject.SetActive(false);
    }
    public void Buy()
    {
        if (_buyableGameField.OpenLevel(_scorreCounter.TotalScore))
        {
            _scorreCounter.ReductionScorre((int)_buyableGameField.Price);
            Cancel();
        }
        else
        {
            _confirmingBuyPanel.PlayDangeringAnimation();
        }
    }
}
