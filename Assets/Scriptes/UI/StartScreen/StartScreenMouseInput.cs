using UnityEngine;

public class StartScreenMouseInput : MonoBehaviour
{
    [SerializeField] private ChoseGameField _choseGameField;
    private Vector2 _startPosition;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
            _choseGameField.ScrollingStart(_startPosition);
        }
        if(Input.GetMouseButton(0))
        {
            _choseGameField.ScrollingFields((Vector2)Input.mousePosition - _startPosition);
        }
        if(Input.GetMouseButtonUp(0))
        {
            _choseGameField.ScrollingFinish(Input.mousePosition);
        }
    }
}
