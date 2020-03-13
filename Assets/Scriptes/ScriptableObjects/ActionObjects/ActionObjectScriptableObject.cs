using UnityEngine;

[CreateAssetMenu(fileName = "new Action Object", menuName = "BuyableObjects/ActionObject")]
public class ActionObjectScriptableObject : ScriptableObject, IGeneratedBy
{
    [SerializeField] private GameObject _actionObjectAvatar;
    [SerializeField] private MonoBehaviour _actionObjectPrefab;
    [SerializeField] private UsedPlace _place;

    public GameObject Avatar => _actionObjectAvatar;
    public ActionObject ActionObject => _actionObjectPrefab as ActionObject;
    UsedPlace IGeneratedBy.UsedPlace => _place;
}

public enum UsedPlace
{
    ActionObjectFree,
    ActionObjectBound,
    SpawnObjectFree,
    SpawnObjectBound,
    Upgrade
}

public enum TypeForAnchor
{
    ActionObject,
    SpawnObject
}

