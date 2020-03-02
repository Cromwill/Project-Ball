using UnityEngine;

public class ChoseGameField : MonoBehaviour
{
    [SerializeField] private GameField[] _gameFields;
    [SerializeField] private RectTransform _startPosition;

    private void Start()
    {
        foreach (var fields in _gameFields)
        {
            fields.MoveSelf(new Vector2(0, 0), _startPosition.position);
        }
    }

    public void ScrollingFields(Vector2 direction)
    {
        if (_gameFields[0].Position.x > _startPosition.position.x && direction.x > 0)
            return;
        else if(_gameFields[_gameFields.Length - 1].Position.x <_startPosition.position.x && direction.x < 0)
            return;
        else
        {
            for(int i = 0; i < _gameFields.Length; i++)
            {
                _gameFields[i].MoveSelf(new Vector2(direction.x, 0), _startPosition.position);
            }
        }
    }

    public void ScrollingStart(Vector2 startPosition)
    {
        for (int i = 0; i < _gameFields.Length; i++)
        {
            _gameFields[i].PreparationToMove();
        }
    }

    public void ScrollingFinish(Vector2 finishPosition)
    {

    }
}
