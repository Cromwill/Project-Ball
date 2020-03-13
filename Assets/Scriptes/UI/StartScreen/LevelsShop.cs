using UnityEngine;

[RequireComponent(typeof(ChoseGameField))]
public class LevelsShop : MonoBehaviour
{
    [SerializeField] private StartScreenScorreCounter _scorreCounter;

    public void Buy(GameField gameField)
    {
        if (gameField.OpenLevel((int)_scorreCounter.TotalScorre))
            _scorreCounter.ReductionScorre((int)gameField.Price);
    }
}
