using UnityEngine;

public interface IGeneratedBy
{
    GameObject Avatar { get;}
    ActionObject ActionObject { get; }
    IBuyable BuyableObject { get; }
    ActionObjectScriptableObject.ActionObjectType GetType { get; }
}
