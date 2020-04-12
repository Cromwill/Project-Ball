using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private MoverAroundLocation _objectMove;
    [SerializeField] private ActionObjectSpawner _objectSpawner;

    private bool _isFindObject;

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _objectMove.SetMovePositionData(position);
                _isFindObject = SearchObject(position);
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (!_isFindObject)
                {
                    _objectMove.Move(position);
                }
            }
        }
    }

    private bool SearchObject(Vector2 inputPosition)
    {
        var hit = Physics2D.OverlapCircle(inputPosition, 0.1f);
        if (hit != null)
        {
            if (hit.GetComponent<IActionObjectAnchor>() != null && _objectSpawner.IsUsing)
            {
                _objectSpawner.ChangeAvatarPositionOnScene(hit.GetComponent<IActionObjectAnchor>());
                return true;
            }
            else if (hit.GetComponent<ActionObject>() != null && !_objectSpawner.IsUsing)
            {
                _objectSpawner.DeletedObject(hit.GetComponent<ActionObject>());
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
}
