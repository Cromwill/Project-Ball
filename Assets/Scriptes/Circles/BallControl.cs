using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField]
    private BallPlayer _player;

    private List<Ball> _currentBalls = new List<Ball>();
    private List<Ball> _nextBalls = new List<Ball>();
    private ObjectFinder _objectFinder;

    private void OnEnable()
    {
        _objectFinder = GetComponentInChildren<ObjectFinder>();
        _objectFinder.ObjectFinded += Action;
    }

    private void OnDisable()
    {
        _objectFinder.ObjectFinded -= Action;
    }

    private void FixedUpdate()
    {

        if(Input.GetMouseButton(0))
        {
            Vector2 inputPosition = GetToWorldPosition(Input.mousePosition);
            _objectFinder.Search(inputPosition);
            UseBall(inputPosition);
        }
        if(!Input.GetMouseButton(0))
        {
            _objectFinder.NotSearch();
            ThrowTheBall();
        }
    }

    public void Action(Ball ball, Vector2 position)
    {
        if (!_currentBalls.Contains(ball))
        {
            _currentBalls.Add(ball);
        }
    }

    private Vector2 GetToWorldPosition(Vector3 position)
    {
        return Camera.main.ScreenToWorldPoint(position);
    }

    private void UseBall(Vector2 position)
    {
        for(int i = 0; i < _currentBalls.Count; i++)
        {
            _currentBalls[i].Action(position);
        }
    }

    private void ThrowTheBall()
    {
        for (int i = 0; i < _currentBalls.Count; i++)
        {
            _currentBalls[i].InAction();
            _currentBalls.RemoveAt(i);
        }
    }
}
