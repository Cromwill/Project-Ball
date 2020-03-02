using UnityEngine;

public interface IActionObjectAnchor
{
    Transform Avatar { get; set; }
    bool IsFree { get; set; }
    ActionObjectScriptableObject.ActionObjectType GetType { get; }
    Vector2 GetPosition();
    void ToggleColor();
}
