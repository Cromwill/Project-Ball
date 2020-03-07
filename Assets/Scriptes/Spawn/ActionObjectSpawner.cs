using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActionObjectSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap _actionObjectTilemap;
    [SerializeField] private Tilemap _spawnObjectTilemap;
    [SerializeField] private PoolForObjects _spawnPool;
    [SerializeField] private GameEconomy _gameEconomy;
    [SerializeField] private ActionObjectScriptableObject _deletedObject;

    public event Action DeletingObject;

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
        if (_currenObject != null)
            _currenObject.ChangeAnchor(anchor);
    }

    public void SetObjectOnScene(IGeneratedBy actionObject)
    {
        FillingObjectSpawner(actionObject, null);
    }

    public void DeletedObject(ActionObject actionObject)
    {
        var anchor = _anchorsForActionObject.Where(a => !a.IsFree).First(a => a.InstalledFacility.Equals(actionObject as IUpgradeable));
        FillingObjectSpawner(_deletedObject, anchor);
        OnDeletingObject();
    }

    public void ConfirmSetObject()
    {
        if (_currenObject.IsActionObject() && !_currenObject.IsUpgrade())
            _currenObject.SetObjectOnScene(Instantiate(_currenObject.ActionObject));
        else if (_currenObject.IsUpgrade())
            _currenObject.SetObjectOnScene(_currenObject.ActionObject as UpgradeObject);
        else
            _currenObject.SetObjectOnScene(_spawnPool.GetObject() as ObjectPool);

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

    private void FillingObjectSpawner(IGeneratedBy actionObject, IActionObjectAnchor anchor)
    {
        var avatar = Instantiate(actionObject.Avatar).GetComponent<Transform>();
        _currenObject = new ObjectSpawner(actionObject, avatar);
        anchor = anchor ?? GetCorrectAnchorsArray().First();
        _currenObject.ChangeAnchor(anchor);
        ToggleAnchors();
    }

    private void OnDeletingObject()
    {
        DeletingObject?.Invoke();
    }

    private IActionObjectAnchor[] GetCorrectAnchorsArray()
    {
        return _currenObject.IsActionObject() ? _anchorsForActionObject.Where(a => a.IsFree == !_currenObject.IsUpgrade()).ToArray() : 
            _anchorsForSpawnObject.Where(a => a.IsFree == !_currenObject.IsUpgrade()).ToArray();
    }

    private void ToggleAnchors()
    {
        var anchors = GetCorrectAnchorsArray();
        for (int i = 0; i < anchors.Length; i++)
            anchors[i].ToggleColor();
    }
}
