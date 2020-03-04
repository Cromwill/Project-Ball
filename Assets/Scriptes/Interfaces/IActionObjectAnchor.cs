using UnityEngine;

public interface IActionObjectAnchor
{
    bool IsFree { get; set; }
    IChangeable InstalledFacility { get;}
    ActionObjectScriptableObject.ActionObjectType GetType { get; }
    Vector2 GetPosition();
    void ToggleColor();
    void SetChangeableObject(IChangeable changeableObject);
}
