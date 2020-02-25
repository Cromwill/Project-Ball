using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField] private ObjectMove _objectMove;
    [SerializeField] private ActionObjectSpawner _objectSpawner;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _objectMove.SetMovePositionData(position);
        }

        if(Input.GetMouseButton(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!SearchObject(position))
            {
                _objectMove.Move(position);
            }
        }
    }

    private bool SearchObject(Vector2 inputPosition)
    {

        var v = Physics2D.OverlapCircle(inputPosition, 0.1f);
        if (v != null)
        {
            if (v.GetComponent<IActionObjectAnchor>() != null && _objectSpawner.IsUsing)
            {
                _objectSpawner.ChangeAvatarPositionOnScene(v.GetComponent<IActionObjectAnchor>());
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false;
    }
}
