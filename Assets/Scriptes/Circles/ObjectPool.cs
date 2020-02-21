using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour
{
    [SerializeField]
    protected PoolObject _prefab;
    [SerializeField]
    protected T[] _objectArray;

    protected Transform _selfTransform;

    private void OnEnable()
    {
        _selfTransform = GetComponent<Transform>();
    }

    protected abstract void GenerateObjectPool(int count);
    protected abstract T GetObject(int index);
    protected abstract T[] GetObjects(int count);

}
