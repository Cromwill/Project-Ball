using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(LevelData))]
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
    private string _levelName;

    public bool IsUsing => _currenObject != null;

    private void OnEnable()
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
            _currenObject.SetObjectOnScene(Instantiate(_currenObject.ActionObject), _levelName);
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

    public void Save(string level)
    {
        for (int i = 0; i < _anchorsForActionObject.Length; i++)
        {
            if (!_anchorsForActionObject[i].IsFree)
            {
                CustomPlayerPrefs.SetFloat(_levelName + "_actionAnchorIndex_" + i + "_positionX", _anchorsForActionObject[i].GetPosition().x);
                CustomPlayerPrefs.SetFloat(_levelName + "_actionAnchorIndex_" + i + "_positionY", _anchorsForActionObject[i].GetPosition().y);
                CustomPlayerPrefs.SetString(_levelName + "_actionAnchorIndex_" + i + "_object",
                    (_anchorsForActionObject[i].InstalledFacility as ActionObject).name.Split(new char[] { '(', ')' }, System.StringSplitOptions.RemoveEmptyEntries)[0]);
            }
            else
            {
                PlayerPrefs.DeleteKey(_levelName + "_actionAnchorIndex_" + i + "_positionX");
                PlayerPrefs.DeleteKey(_levelName + "_actionAnchorIndex_" + i + "_positionY");
                PlayerPrefs.DeleteKey(_levelName + "_actionAnchorIndex_" + i + "_object");
            }
        }
    }

    public void Load(string level)
    {
        for (int i = 0; i < _anchorsForActionObject.Length; i++)
        {
            if (PlayerPrefs.HasKey(level + "_actionAnchorIndex_" + i + "_positionX"))
            {
                Vector2 savedPosition = new Vector2(PlayerPrefs.GetFloat(level + "_actionAnchorIndex_" + i + "_positionX"),
                    PlayerPrefs.GetFloat(level + "_actionAnchorIndex_" + i + "_positionY"));
                var actionObject = Instantiate(Resources.Load<ActionObject>("Prefabs/ActionObject/" + PlayerPrefs.GetString(level + "_actionAnchorIndex_" + i + "_object")));
                var anchor = _anchorsForActionObject.Where(a => a.GetPosition() == savedPosition).First();
                actionObject.SetPosition(anchor.GetPosition());
                anchor.SetChangeableObject(actionObject);
            }
        }
        _levelName = level;
    }

    private void EndUse()
    {
        Destroy(_currenObject.Avatar.gameObject);
        ToggleAnchors();
        Save(_levelName);
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
