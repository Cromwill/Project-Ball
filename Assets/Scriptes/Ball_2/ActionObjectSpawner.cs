using UnityEngine;
using UnityEngine.Tilemaps;

public class ActionObjectSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap _actionObjectTilemap;
    [SerializeField] PhysicsActionScriptableObject[] _physicsActionObjects;

    public bool IsUsing { get; private set; }

    private IActionObjectAnchor[] _anchors;
    private PhysicsActionScriptableObject _currentActionObject;
    private IActionObjectAnchor _currentAnchor;
    private GameObject _currentAvatar;

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
        _anchors = _actionObjectTilemap.GetComponentsInChildren<IActionObjectAnchor>();
    }

    public void ChangeAvatarPositionOnScene(IActionObjectAnchor anchor)
    {
        if(IsUsing && anchor.IsFree)
        {
            _currentAnchor = anchor;
            _currentAvatar.GetComponent<Transform>().position = anchor.GetPosition();
        }
    }

    private void SetObjectOnScene(PhysicsActionScriptableObject actionObject)
    {
        IsUsing = true;
        _currentActionObject = actionObject;
        AnchorsChangeState();
        _currentAvatar = Instantiate(actionObject.Avatar);
        for(int i = 0; i< _anchors.Length; i++)
        {
            if(_anchors[i].IsFree)
            {
                _currentAvatar.GetComponent<Transform>().position = _anchors[i].GetPosition();
                _currentAnchor = _anchors[i];
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
                var v = Instantiate(_currentActionObject.ActionObject);
                v.SetPosition(_currentAnchor.GetPosition());
                _currentAnchor.IsFree = false;
                Destroy(_currentAvatar);
                AnchorsChangeState();
                IsUsing = false;
            }
            else
            {
                Destroy(_currentAvatar);
                AnchorsChangeState();
                IsUsing = false;
            }
        }
    }

    private void AnchorsChangeState()
    {
        for(int i = 0; i < _anchors.Length; i++)
        {
            _anchors[i].ChangeColor();
        }
    }
}
