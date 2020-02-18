using System.Collections.Generic;
using UnityEngine;

public class BallPlayer : MonoBehaviour
{
    [SerializeField]
    private Spawn _spawner;

    private void Start()
    {
        _spawner.SpawnCircle();
    }

    public void Action(Vector2 position, IList<Ball> balls)
    {
        foreach(var ball in balls)
        {
            ball.Action(position);
        }
    }

    public void InAction(IList<Ball> balls)
    {
        foreach (var ball in balls)
        {
            ball.InAction();
        }
    }
}
