using UnityEngine;

public interface IActionObjectAnchor
{
    bool IsFree { get; set; }
    IChangeable InstalledFacility { get;}
    TypeForAnchor GetAnchorType { get; }
    Vector2 GetPosition();
    void ToggleColor();
    void SetChangeableObject(IChangeable changeableObject);
}
