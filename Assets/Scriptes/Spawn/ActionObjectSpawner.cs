using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActionObjectSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap _actionObjectTilemap;
    [SerializeField] private Tilemap _spawnObjectTilemap;
    [SerializeField] private PoolForObjects _spawnPool;
    [SerializeField] private GameEconomy _gameEconomy;

    private IActionObjectAnchor[] _anchorsForActionObject;
    private IActionObjectAnchor[] _anchorsForSpawnObject;
    private ObjectSpawner _currenObject;

    public bool IsUsing => _currenObject != null;

    private void Start()
    {
        _anchorsForActionObject = _actionObjectTilemap.GetComponentsInChildren<IActionObjectAnchor>();
        _anchorsForSpawnObject = _spawnObjectTilemap.GetComponentsInChildren<IActionObjectAnchor>();
    }

    public void ChangeAvatarPositionOnScene(IActionObjectAnchor anchor)
    {
        Debug.Log("ChangeAnchoronScene");
        if (_currenObject != null)
            _currenObject.ChangeAnchor(anchor);
    }

    public void SetObjectOnScene(IGeneratedBy actionObject)
    {
        var avatar = Instantiate(actionObject.Avatar).GetComponent<Transform>();
        _currenObject = new ObjectSpawner(actionObject, avatar);
        ToggleAnchors();
        _currenObject.ChangeAnchor(GetCorrectAnchorsArray().First(a => a.IsFree == !_currenObject.IsUpgrade()));
    }

    public void ConfirmSetObject()
    {
        if (_currenObject.IsActionObject())
            _currenObject.SetObjectOnScene(Instantiate(_currenObject.ActionObject));
        else if (_currenObject.IsUpgrade())
            _currenObject.SetObjectOnScene(_currenObject.ActionObject);
        else
            _currenObject.SetObjectOnScene(_spawnPool.GetObject() as IBuyable);

        _gameEconomy.OnPurchaseCompleted(_currenObject.BuyableObject);
        EndUse();
    }

    public void DeclineSetObject()
    {
        EndUse();
    }

    private void EndUse()
    {
        Destroy(_currenObject.Avatar.gameObject);
        ToggleAnchors();
        _currenObject = null;
    }

    private IActionObjectAnchor[] GetCorrectAnchorsArray()
    {
        return _currenObject.IsActionObject() ? _anchorsForActionObject : _anchorsForSpawnObject;
    }

    private void ToggleAnchors()
    {
        var anchors = GetCorrectAnchorsArray();
        for (int i = 0; i < anchors.Length; i++)
            anchors[i].ToggleColor();
    }
}
