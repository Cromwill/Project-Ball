using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private MoverAroundLocation _objectMove;
    [SerializeField] private ActionObjectSpawner _actionObjectSpawner;
    [SerializeField] private SpawnObjectSpawner _spawnObjectSpawner;

    private bool _isFindObject;

    private void Update()
    {
        //if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
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
                    _spawnObjectSpawner.ChangeCameraPosition();
                }
            }
        }
    }

    private bool SearchObject(Vector2 inputPosition)
    {
        var hit = Physics2D.OverlapCircle(inputPosition, 0.1f);
        if (hit != null)
        {
            IActionObjectAnchor anchor = hit.GetComponent<IActionObjectAnchor>();

            if (anchor != null)
            {
                if (_actionObjectSpawner.IsUsing)
                    _actionObjectSpawner.ChangeAvatarPositionOnScene(anchor);
                else if (_spawnObjectSpawner.IsUsing)
                    _spawnObjectSpawner.ChangeAvatarPositionOnScene(anchor);
                return true;
            }
            else if (hit.GetComponent<ActionObject>() != null && !IsSpawnersUsing())
            {
                _actionObjectSpawner.DeletedObject(hit.GetComponent<ActionObject>());
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    private bool IsSpawnersUsing() => _actionObjectSpawner.IsUsing || _spawnObjectSpawner.IsUsing;
}
