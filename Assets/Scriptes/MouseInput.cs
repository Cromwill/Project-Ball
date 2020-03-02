using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private MoverAroundLocation _objectMove;
    [SerializeField] private ActionObjectSpawner _objectSpawner;

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _objectMove.SetMovePositionData(position);
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (!SearchObject(position))
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
            else
            {
                return false;
            }
        }
        else
            return false;
    }
}
