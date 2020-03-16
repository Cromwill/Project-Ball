using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LevelsShop))]
public class ChoseGameField : MonoBehaviour
{
    [SerializeField] private GameField[] _gameFields;

    public GameField[] GameFields => _gameFields;

    public string[] GetOpenLevelNames()
    {
        return _gameFields.Where(a => a.IsOpenLevel).Select(a => a.Name).ToArray();
    }
}
