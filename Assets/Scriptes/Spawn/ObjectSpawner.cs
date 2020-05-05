using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(GameEconomy), typeof(LevelData))]
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] protected Tilemap _objectTilemap;

    protected GameEconomy _gameEconomy;
    protected IActionObjectAnchor[] _anchors;
    protected ObjectSpawnerData _currentObject;
    protected string _levelName;

    public event Action<TypeForAnchor> OutOfAnchors;

    public bool IsUsing => _currentObject != null;

    private void Awake()
    {
        _anchors = _objectTilemap.GetComponentsInChildren<IActionObjectAnchor>();

        #region check dubleAnchor
        List<Vector2> positions = new List<Vector2>();
        for (int i = 0; i < _anchors.Length; i++)
        {
            if (!positions.Contains(_anchors[i].GetPosition()))
                positions.Add(_anchors[i].GetPosition());
            else
                Debug.Log(_anchors[i].GetPosition());
        }
        #endregion

        _gameEconomy = GetComponent<GameEconomy>();
    }

    public void ChangeAvatarPositionOnScene(IActionObjectAnchor anchor)
    {
        if (_anchors.Contains(anchor))
            _currentObject.ChangeAnchor(anchor);
    }
    public virtual void SetObjectOnScene(IGeneratedBy actionObject) => FillingObjectSpawner(actionObject, null);


    public virtual void DeletedObject(ActionObject actionObject) { }

    public virtual void ConfirmSetObject()
    {
        _gameEconomy.OnPurchaseCompleted(_currentObject.ActionObject);
        EndUse();
    }

    public virtual void Save(string level) { }

    public virtual void Load(string level) { }

    public void EndUse()
    {
        if (IsUsing)
        {
            Destroy(_currentObject.Avatar.gameObject);
            ToggleAnchors();
            Save(_levelName);
            if (_anchors.Where(a => a.IsFree).Count() == 0)
                OnOutOfAnchors(_anchors.First().GetAnchorType);

            _currentObject = null;
        }
    }

    public void EnoughAnchors()
    {
        if (_anchors.Where(a => a.IsFree).Count() == 0)
            OnOutOfAnchors(_anchors.First().GetAnchorType);
    }

    protected virtual void FillingObjectSpawner(IGeneratedBy actionObject, IActionObjectAnchor anchor)
    {
        var avatar = Instantiate(actionObject.Avatar).GetComponent<Transform>();
        _currentObject = new ObjectSpawnerData(actionObject, avatar);
        anchor = anchor ?? GetCorrectAnchorsArray().First();
        _currentObject.ChangeAnchor(anchor);
        ToggleAnchors();
    }

    protected void ToggleAnchors()
    {
        for (int i = 0; i < _anchors.Length; i++)
            _anchors[i].ToggleColor();
    }

    protected virtual IActionObjectAnchor[] GetCorrectAnchorsArray()
    {
        return _anchors.Where(a => a.IsFree == !_currentObject.IsUpgrade()).ToArray();
    }

    protected void OnOutOfAnchors(TypeForAnchor anchorType) => OutOfAnchors?.Invoke(anchorType);
}

