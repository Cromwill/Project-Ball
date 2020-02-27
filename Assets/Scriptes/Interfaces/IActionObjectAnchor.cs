using UnityEngine;

public interface IActionObjectAnchor
{

    bool IsFree { get; set; }
    Vector2 GetPosition();
    void Toggle();
}
