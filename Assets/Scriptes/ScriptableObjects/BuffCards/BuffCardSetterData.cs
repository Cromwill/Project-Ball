using UnityEngine;

[CreateAssetMenu(fileName = "new BuffCardSetterData", menuName = "Create BuffCardSetterData")]
public class BuffCardSetterData : ScriptableObject
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _settedSprite;
}
