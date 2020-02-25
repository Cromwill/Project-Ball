using UnityEngine;

[CreateAssetMenu(fileName = "new Physics Action Object", menuName = "ActionObject")]
public class PhysicsActionScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject _ActionObjectAvatar;
    [SerializeField] private ActionObjectBase _ActionObject;

    public GameObject Avatar
    {
        get => _ActionObjectAvatar;
    }

    public ActionObjectBase ActionObject
    {
        get => _ActionObject;
    }

}
