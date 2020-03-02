using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActiveObject
{
    void Action(Vector2 target);
    void InAction();
}
