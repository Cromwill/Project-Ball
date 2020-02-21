using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Spawn : MonoBehaviour
{
    [SerializeField]
    private int _ballCount;
    [SerializeField]
    private ObjectPoolForBalls _pool;

    private Vector2 _spawnPositionMin;
    private Vector2 _spawnPositionMax;

    private void OnEnable()
    {
        _pool.BallsGenerated += SpawnBalls;
    }

    private void OnDisable()
    {
        _pool.BallsGenerated -= SpawnBalls;
    }

    private void Start()
    {
        Vector2 halfSize = GetComponent<BoxCollider2D>().size * 0.5f;
        Vector2 centerPosition = GetComponent<Transform>().position;
        _spawnPositionMin = centerPosition + (halfSize * -1);
        _spawnPositionMax = centerPosition + (halfSize * 1);
        _pool.Generate(_ballCount * 2);
    }

    public void SpawnBalls()
    {
        Debug.Log("SpawnBall");
        StartCoroutine(SpawnObjects(2.0f/ _ballCount, _ballCount));
    }

    private IEnumerator SpawnObjects(float time, int count)
    {
        for (int i = 0; i < count; i++)
        {
            _pool.GetObject().LeaveThePoll(GetRandomPosition(_spawnPositionMin, _spawnPositionMax));
            yield return new WaitForSeconds(time);
        }
    }

    private Vector2 GetRandomPosition(Vector2 min, Vector2 max)
    {
        return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }
}
