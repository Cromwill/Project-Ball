using UnityEngine;

public class ObjectSpawner
{
    private IGeneratedBy _actionObject;
    private IActionObjectAnchor _anchor;

    public Transform Avatar { get; }
    public ActionObject ActionObject => _actionObject.ActionObject;

    public ObjectSpawner(IGeneratedBy actionObject, Transform avatar)
    {
        _actionObject = actionObject;
        Avatar = avatar;
    }

    public bool IsActionObject() => _actionObject.UsedPlace == UsedPlace.ActionObjectBound || _actionObject.UsedPlace == UsedPlace.ActionObjectFree;

    public bool IsUpgrade() => _actionObject.UsedPlace == UsedPlace.ActionObjectBound || _actionObject.UsedPlace == UsedPlace.SpawnObjectBound;

    public void ChangeAnchor(IActionObjectAnchor anchor)
    {
        if (CompairObjectWithAnchor(_actionObject.UsedPlace, anchor.GetAnchorType) && anchor.IsFree)
        {
            if (!IsUpgrade())
                ChangeCurrentAnchor(anchor);
        }

        if (IsUpgrade() && !anchor.IsFree)
            ChangeCurrentAnchor(anchor);
    }

    public void SetObjectOnScene(UpgradeObject upgradeObject)
    {
        _anchor.InstalledFacility.Upgrade(upgradeObject.ChangingValue);
        if (IsActionObject())
            _anchor.IsFree = true;
    }

    public void SetObjectOnScene(ActionObject actionObject, bool isAction)
    {
        if (isAction)
            actionObject.SetPosition(_anchor.GetPosition());
        else
            (actionObject as Spawn).LeaveThePoolAndRun(_anchor.GetPosition());

        _anchor.SetChangeableObject(actionObject as IUpgradeable);
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
        Avatar.position = anchor.GetPosition();
    }
}
