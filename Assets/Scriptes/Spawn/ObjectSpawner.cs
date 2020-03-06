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
        return _actionObject.UsedPlace == UsedPlace.ActionObjectBound || _actionObject.UsedPlace == UsedPlace.ActionObjectFree;
    }

    public bool IsUpgrade()
    {
        return _actionObject.UsedPlace == UsedPlace.ActionObjectBound || _actionObject.UsedPlace == UsedPlace.SpawnObjectBound;
    }

    public void ChangeAnchor(IActionObjectAnchor anchor)
    {
        if (CompairObjectWithAnchor(_actionObject.UsedPlace, anchor.GetAnchorType) && anchor.IsFree)
            ChangeCurrentAnchor(anchor);

        if (IsUpgrade() && !anchor.IsFree)
            ChangeCurrentAnchor(anchor);
    }

    public void SetObjectOnScene(UpgradeObject upgradeObject)
    {
        _anchor.InstalledFacility.ChangeCondition(upgradeObject.ChangingValue);
        if (IsActionObject())
            _anchor.IsFree = true;
    }

    public void SetObjectOnScene(ActionObject actionObject)
    {
        actionObject.SetPosition(_anchor.GetPosition()); ;
        _anchor.SetChangeableObject(actionObject as IChangeable);
    }

    public void SetObjectOnScene(ObjectPool poolObject)
    {
        poolObject.LeaveThePoolAndRun(_anchor.GetPosition());
        _anchor.SetChangeableObject(poolObject as IChangeable);
    }

    private bool CompairObjectWithAnchor(UsedPlace placeForObject, TypeForAnchor typeForAnchor)
    {
        if (typeForAnchor == TypeForAnchor.ActionObject)
            return placeForObject == UsedPlace.ActionObjectBound || placeForObject == UsedPlace.ActionObjectFree;
        else if (typeForAnchor == TypeForAnchor.SpawnObject)
            return placeForObject == UsedPlace.SpawnObjectBound || placeForObject == UsedPlace.SpawnObjectFree;
        else
            return false;
    }

    private void ChangeCurrentAnchor(IActionObjectAnchor anchor)
    {
        _anchor = anchor;
        _avatarTransform.position = anchor.GetPosition();
    }
}
