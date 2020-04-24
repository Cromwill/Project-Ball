using UnityEngine;

public class ActionObject : MonoBehaviour, IBuyable, IUpgradeable
{
    [SerializeField] protected float _price;
    [SerializeField] protected string _name;
    [SerializeField] protected ActionObjectType _type;

    protected Transform _selfTransform;

    public float Price  => _price;
    public string LevelName => _name;
    public ActionObjectType ObjectType => _type;

    private void OnEnable()
    {
        _selfTransform = GetComponent<Transform>();
    }

    public virtual void SetPosition(Vector2 position)
    {
        _selfTransform.position = position;
    }

    public virtual void Upgrade(float value)
    {
        Destroy(gameObject);
    }

    public virtual bool IsCanUpgrade()
    {
        throw new System.NotImplementedException();
    }
}

public enum ActionObjectType
{
    Phisics,
    Action,
    Spawn,
    UpgradeSpawn,
    UpgradeScorre,
    Deleted
}
