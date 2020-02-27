using UnityEngine;

interface IGeneratedBy
{
    GameObject Avatar { get; }
    ActionObject ActionObject { get; }

    ActionObjectScriptableObject.ActionObjectType GetType { get; }
}
