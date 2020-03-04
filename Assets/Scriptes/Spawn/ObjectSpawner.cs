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

    public bool IsUpgrade()
    {
        return _actionObject.GetType == ActionObjectScriptableObject.ActionObjectType.EvolveObject;
    }

    public void ChangeAnchor(IActionObjectAnchor anchor)
    {
        Debug.Log("Type - " + _actionObject.GetType);

        if (_actionObject.GetType == anchor.GetType && anchor.IsFree)
            ChangeCurrentAnchor(anchor);

        if (_actionObject.GetType == ActionObjectScriptableObject.ActionObjectType.EvolveObject && !anchor.IsFree)
        {
            ChangeCurrentAnchor(anchor);
            Debug.Log("change Avatar");
        }
    }

    public void SetObjectOnScene(IBuyable actionObject)
    {
        if (IsActionObject())
            (actionObject as ActionObject).SetPosition(_anchor.GetPosition());
        else if (IsUpgrade())
        {
            _anchor.InstalledFacility.ChangeCondition((actionObject as UpgradeObject).ChangingValue);
            return;
        }
        else
            (actionObject as ObjectPool).LeaveThePoolAndRun(_anchor.GetPosition());

        if (!IsUpgrade())
            _anchor.SetChangeableObject(actionObject as IChangeable);
        _anchor.IsFree = false;
    }

    private void ChangeCurrentAnchor(IActionObjectAnchor anchor)
    {
        _anchor = anchor;
        _avatarTransform.position = anchor.GetPosition();
    }
}
