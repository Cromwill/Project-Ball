using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LevelsShop))]
public class ChoseGameField : MonoBehaviour
{
    [SerializeField] private GameField[] _gameFields;
    [SerializeField] private RectTransform _startPosition;

    public GameField[] GameFields => _gameFields;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(_gameFields[0].Name + "_isOpen"))
            CustomPlayerPrefs.SetInt(_gameFields[0].Name + "_isOpen", 1);

        foreach (var fields in _gameFields)
            fields.MoveSelf(new Vector2(0, 0), _startPosition.position);
    }

    public void ScrollingFields(Vector2 direction)
    {
        if (isCanMove(direction))
        {
            for (int i = 0; i < _gameFields.Length; i++)
                _gameFields[i].MoveSelf(new Vector2(direction.x, 0), _startPosition.position);
        }
    }

    public void ScrollingStart(Vector2 startPosition)
    {
        for (int i = 0; i < _gameFields.Length; i++)
            _gameFields[i].PreparationToMove();
    }

    public string[] GetOpenLevelNames()
    {
        return _gameFields.Where(a => a.IsOpenLevel).Select(a => a.Name).ToArray();
    }

    private bool isCanMove(Vector2 direction)
    {
        if (_gameFields[0].Position.x > _startPosition.position.x && direction.x > 0)
            return false;
        else if (_gameFields[_gameFields.Length - 1].Position.x < _startPosition.position.x && direction.x < 0)
            return false;
        else
            return true;
    }
}
