using UnityEngine;

[CreateAssetMenu(fileName = "new Action Object", menuName = "BuyableObjects/ActionObject")]
public class ActionObjectScriptableObject : ScriptableObject, IGeneratedBy
{
    [SerializeField] private GameObject _actionObjectAvatar;
    [SerializeField] private MonoBehaviour _actionObjectPrefab;
    [SerializeField] private ActionObjectType _type;

    public IBuyable BuyableObject => (IBuyable)_actionObjectPrefab;
    public GameObject Avatar => _actionObjectAvatar;
    public ActionObject ActionObject => BuyableObject as ActionObject;
    ActionObjectType IGeneratedBy.GetType => _type;

    public enum ActionObjectType
    {
        ActionObject,
        SpawnObject,
        EvolveObject
    }
}
