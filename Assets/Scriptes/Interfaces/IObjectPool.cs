using UnityEngine;

public interface IObjectPool
{
    bool IsInThePool { get;}
    IPoolForObjects SelfObjectForPool{get;set;}
    void LeaveThePoll(Vector2 position);
    void ReturnToPool(Vector2 position);
    Vector2 GetPosition();
    void StartUsing<T, U>(T value, U valueU);
}

