using UnityEngine;

public interface IObjectPool
{
    bool IsInThePool { get;}
    void LeaveThePool(Vector2 position);
    void ReturnToPool(Vector2 position);
    Vector2 GetPosition();
    IPoolForObjects SelfObjectForPool { get; set; }
}

