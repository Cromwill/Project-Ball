using UnityEngine;
using UnityEngine.Tilemaps;

public class ActionObjectSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap _actionObjectTilemap;
    [SerializeField] private Tilemap _spawnObjectTilemap;
    [SerializeField] private PoolForObjects _spawnPool;

    public bool IsUsing { get; private set; }

    private IActionObjectAnchor[] _anchorsForActionObject;
    private IActionObjectAnchor[] _anchorsForSpawnObject;
    private IGeneratedBy _currentActionObject;
    private IActionObjectAnchor _currentAnchor;
    private Transform _currentAvatar;
    private ActionObjectScriptableObject.ActionObjectType _currentObjectType;

    private bool _currenObjectIsAction => _currentActionObject.GetType == ActionObjectScriptableObject.ActionObjectType.ActionObject ? true : false;

    private void OnEnable()
    {
        ActionObjectShopButton.UsingActionObject += SetObjectOnScene;
        ObjectUIMenuElement.Confirm += ConfirmSetObject;
    }
    private void OnDisable()
    {
        ActionObjectShopButton.UsingActionObject -= SetObjectOnScene;
        ObjectUIMenuElement.Confirm -= ConfirmSetObject;
    }

    private void Start()
    {
        _anchorsForActionObject = _actionObjectTilemap.GetComponentsInChildren<IActionObjectAnchor>();
        _anchorsForSpawnObject = _spawnObjectTilemap.GetComponentsInChildren<IActionObjectAnchor>();
    }

    public void ChangeAvatarPositionOnScene(IActionObjectAnchor anchor)
    {
        if (IsAnchorForActionObject(anchor))
        {
            if (IsUsing && anchor.IsFree)
            {
                _currentAnchor = anchor;
                _currentAvatar.position = anchor.GetPosition();
            }
        }
    }

    private bool IsAnchorForActionObject(IActionObjectAnchor anchor)
    {
        var anchors = _currenObjectIsAction ? _anchorsForActionObject : _anchorsForSpawnObject;

        for (int i = 0; i < anchors.Length; i++)
        {
            if (anchors[i].Equals(anchor))
                return true;
        }
        return false;
    }

    private void SetObjectOnScene(IGeneratedBy actionObject)
    {
        IsUsing = true;
        _currentActionObject = actionObject;
        AnchorsChangeState();
        _currentAvatar = Instantiate(actionObject.Avatar).GetComponent<Transform>();

        var anchors = _currenObjectIsAction ? _anchorsForActionObject : _anchorsForSpawnObject;

        for (int i = 0; i < anchors.Length; i++)
        {
            if (anchors[i].IsFree)
            {
                _currentAvatar.position = anchors[i].GetPosition();
                _currentAnchor = anchors[i];
                return;
            }
        }
    }

    private void ConfirmSetObject(bool confirm)
    {
        if (IsUsing)
        {
            if (confirm)
            {
                if (_currenObjectIsAction)
                {
                    var actionObject = Instantiate(_currentActionObject.ActionObject);
                    actionObject.SetPosition(_currentAnchor.GetPosition());
                    _currentAnchor.IsFree = false;
                    EndUse();
                }
                else
                {
                    var actionObject = _spawnPool.GetObject();
                    actionObject.LeaveThePoll(_currentAnchor.GetPosition());
                    EndUse();
                }
            }
            else
            {
                EndUse();
            }
        }
    }

    private void EndUse()
    {
        Destroy(_currentAvatar.gameObject);
        AnchorsChangeState();
        IsUsing = false;
    }

    private void AnchorsChangeState()
    {
        var anchors = _currenObjectIsAction ? _anchorsForActionObject : _anchorsForSpawnObject;
        for (int i = 0; i < anchors.Length; i++)
            anchors[i].Toggle();
    }
}
