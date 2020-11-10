using UnityEngine;

[CreateAssetMenu(fileName = "new Action Object", menuName = "BuyableObjects/ActionObject")]
public class ActionObjectScriptableObject : ScriptableObject, IGeneratedBy
{
    [SerializeField] private GameObject _actionObjectAvatar;
    [SerializeField] private MonoBehaviour _actionObjectPrefab;
    [SerializeField] private UsedPlace _place;
    [SerializeField] private bool _isOpeningObject;

    private const string _openingObjectText = "Watch ads to open";

    public GameObject Avatar => _actionObjectAvatar;
    public ActionObject ActionObject => _actionObjectPrefab as ActionObject;
    UsedPlace IGeneratedBy.UsedPlace => _place;
    public string OpeningObjectText => _openingObjectText;
    public bool IsOpeningObject => _isOpeningObject;
}

public enum UsedPlace
{
    Free,
    Bound
}

public enum TypeForAnchor
{
    ActionObject,
    SpawnObject
}

