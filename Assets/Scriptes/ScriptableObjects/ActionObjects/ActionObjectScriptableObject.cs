using UnityEngine;

[CreateAssetMenu(fileName = "new Action Object", menuName = "BuyableObjects/ActionObject")]
public class ActionObjectScriptableObject : ScriptableObject, IGeneratedBy
{
    [SerializeField] private GameObject _actionObjectAvatar;
    [SerializeField] private MonoBehaviour _actionObjectPrefab;
    [SerializeField] private ActionObjectType _type;

    private IBuyable _actionObject => (IBuyable)_actionObjectPrefab;
    public GameObject Avatar => _actionObjectAvatar;
    public ActionObject ActionObject => _actionObject as ActionObject;
    ActionObjectType IGeneratedBy.GetType => _type;

    public enum ActionObjectType
    {
        ActionObject,
        SpawnObject
    }
}
