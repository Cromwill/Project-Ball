using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _ball;
    [SerializeField]
    private int _ballCount;
    [SerializeField]
    private ObjectPool _pool;

    private Rect _spawnRect;
    private BoxCollider2D _selfCollider;

    private void OnValidate()
    {
        if(_ball.GetComponent<Ball>() == null)
        {
            _ball = null;
        }
    }

    private void Start()
    {
        _selfCollider = GetComponent<BoxCollider2D>();
        _spawnRect = new Rect(_selfCollider.transform.position, _selfCollider.size);
    }

    public void SpawnCircle()
    {
        StartCoroutine(SpawnCircle(0.05f, _ballCount));
    }

    private Vector2 GetRandomPosition(Rect rect)
    {
        return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
    }

    private IEnumerator SpawnCircle(float time, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(_ball, GetRandomPosition(_spawnRect), Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }
}
