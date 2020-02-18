using UnityEngine;

public class ObjectPool : MonoBehaviour, IObjectPool
{
    [SerializeField]
    private GameObject _ballPrefab;
    [SerializeField]
    private Ball[] _balls;

    private void OnValidate()
    {
        if (_ballPrefab.GetComponent<Ball>() == null)
            _ballPrefab = null;
    }

    private void GenerateObjectPool()
    {

    }

    public Ball[] GetBalls(int count)
    {
        Ball[] balls = new Ball[count];

        for(int i = 0; i < balls.Length; i++)
        {
            balls[i] = _balls[i];
        }

        return balls;
    }
}
