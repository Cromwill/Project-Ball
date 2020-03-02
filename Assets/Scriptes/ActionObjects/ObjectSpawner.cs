using UnityEngine;

public class ObjectSpawner
{
    private IGeneratedBy _actionObject;
    private IActionObjectAnchor _anchor;
    private Transform _avatarTransform;

    public Transform Avatar => _avatarTransform;
    public ActionObject ActionObject => _actionObject.ActionObject;
    public IBuyable BuyableObject => _actionObject.BuyableObject;

    public ObjectSpawner(IGeneratedBy actionObject, Transform avatar)
    {
        _actionObject = actionObject;
        _avatarTransform = avatar;
    }

    public bool IsActionObject()
    {
        return _actionObject.GetType == ActionObjectScriptableObject.ActionObjectType.ActionObject;
    }

    public void ChangeAnchor(IActionObjectAnchor anchor)
    {
        if (_actionObject.GetType == anchor.GetType)
        {
            _anchor = anchor;
            _avatarTransform.position = anchor.GetPosition();
        }
    }

    public void SetObjectOnScene(IBuyable actionObject)
    {
        if (IsActionObject())
            (actionObject as ActionObject).SetPosition(_anchor.GetPosition());
        else
            (actionObject as ObjectPool).LeaveThePoolAndRun(_anchor.GetPosition());

        _anchor.IsFree = false;
    }
}
