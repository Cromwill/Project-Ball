using UnityEngine;

public class ActionObject : MonoBehaviour, IBuyable, IChangeable
{
    [SerializeField] protected float _price;
    [SerializeField] protected string _name;

    protected Transform _selfTransform;

    public float Price  => _price;
    public string Name => _name;

    private void OnEnable()
    {
        _selfTransform = GetComponent<Transform>();
    }

    public virtual void SetPosition(Vector2 position)
    {
        _selfTransform.position = position;
    }

    public void ChangeCondition(float value)
    {
        throw new System.NotImplementedException();
    }
}
