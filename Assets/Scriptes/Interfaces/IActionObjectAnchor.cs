using UnityEngine;

public interface IActionObjectAnchor
{
    bool IsFree { get; set; }
    IUpgradeable InstalledFacility { get;}
    TypeForAnchor GetAnchorType { get; }
    Vector2 GetPosition();
    void ToggleColor();
    void SetChangeableObject(IUpgradeable changeableObject);
}
