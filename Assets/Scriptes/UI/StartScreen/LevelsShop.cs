using UnityEngine;

[RequireComponent(typeof(ChoseGameField))]
public class LevelsShop : MonoBehaviour
{
    [SerializeField] private StartScreenScorreCounter _scorreCounter;   

    public void Buy(GameField gameField)
    {
        if(_scorreCounter.TotalScorre > gameField.Price)
        {
            gameField.OpenLevel();
            _scorreCounter.ReductionScorre((int)gameField.Price);
        }
    }
}
