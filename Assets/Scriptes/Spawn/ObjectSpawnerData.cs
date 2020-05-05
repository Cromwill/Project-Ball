using UnityEngine;

public class ObjectSpawnerData
{
    private IGeneratedBy _actionObject;
    private IActionObjectAnchor _anchor;

    public Transform Avatar { get; }
    public ActionObject ActionObject => _actionObject.ActionObject;
    public IActionObjectAnchor CurrentAnchor => _anchor;

    public ObjectSpawnerData(IGeneratedBy actionObject, Transform avatar)
    {
        _actionObject = actionObject;
        Avatar = avatar;
    }

    public bool IsUpgrade() => _actionObject.UsedPlace == UsedPlace.Bound;

    public void ChangeAnchor(IActionObjectAnchor anchor)
    {
        if (IsUpgrade() && !anchor.IsFree || !IsUpgrade() && anchor.IsFree)
            ChangeCurrentAnchor(anchor);
    }

    public void UsedUpgrade(bool anchorState)
    {
        if (ActionObject as UpgradeObject != null)
        {
            _anchor.InstalledFacility.Upgrade((ActionObject as UpgradeObject).ChangingValue);
            _anchor.IsFree = anchorState;
        }
    }

    public void SetObjectOnScene(ActionObject actionObject, bool isAction)
    {
        if (isAction)
            actionObject.SetPosition(_anchor.GetPosition());
        else
            (actionObject as Spawn).LeaveThePoolAndRun(_anchor.GetPosition());

        _anchor.SetChangeableObject(actionObject as IUpgradeable);
    }

    private void ChangeCurrentAnchor(IActionObjectAnchor anchor)
    {
        _anchor = anchor;
        Avatar.position = anchor.GetPosition();
    }
}