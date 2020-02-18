using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField]
    private BallPlayer _player;
    [SerializeField]
    private SpriteRenderer _tuchCircle;

    private List<RaycastHit2D> _currentBalls = new List<RaycastHit2D>();
    private List<RaycastHit2D> _nextBalls = new List<RaycastHit2D>();

    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ControlAction();
        }

        if(Input.GetMouseButtonUp(0))
        {
            _tuchCircle.color = new Color(255, 255, 255, 0);
            _player.InAction(ConvertRayCastHit2DToBoll(_currentBalls));

        }

        if(Input.GetMouseButton(0))
        {
            ControlAction();
        }
        if(!Input.GetMouseButton(0))
        {
            _tuchCircle.color = new Color(255, 255, 255, 0);
        }
    }

    private Vector2 GetToWorldPosition(Vector3 position)
    {
        return Camera.main.ScreenToWorldPoint(position);
    }

    private void ControlAction()
    {
        var objects = Physics2D.CircleCastAll(GetToWorldPosition(Input.mousePosition), 1.0f, Vector2.zero).Where(a => a.collider.GetComponent<Ball>() != null);

        _tuchCircle.color = new Color(1, 1, 1, 30f/255f);
        _tuchCircle.GetComponent<Transform>().position = GetToWorldPosition(Input.mousePosition);

        if (_currentBalls == null)
            _currentBalls = objects.ToList();

        _nextBalls = objects.ToList();

        _currentBalls = _currentBalls.Except(_nextBalls).ToList();
        _currentBalls.RemoveAll(x => _nextBalls.Contains(x));

        _player.Action(GetToWorldPosition(Input.mousePosition), ConvertRayCastHit2DToBoll(_nextBalls));
        _player.InAction(ConvertRayCastHit2DToBoll(_currentBalls));
        _currentBalls = _nextBalls;
    }

    private List<Ball> ConvertRayCastHit2DToBoll(List<RaycastHit2D> raycasts)
    {
        List<Ball> balls = new List<Ball>();

            foreach (var v in raycasts)
            {
                balls.Add(v.rigidbody.GetComponent<Ball>());
            }
        return balls;
    }
}
