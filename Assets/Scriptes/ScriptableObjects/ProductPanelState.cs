using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new ProductPanelState", menuName = "Create ProductPanelState")]
public class ProductPanelState : ScriptableObject
{
    [SerializeField] protected Sprite[] _buttonsSprites;
    [SerializeField] protected Sprite[] _checkBoxSprites;
    [SerializeField] private Color[] _textColor;
    [SerializeField] private string _closedMessage;

    public Sprite[] ButtonSprites => _buttonsSprites;
    public Sprite[] CheckBoxSprites => _checkBoxSprites;
    public Color[] TextColor => _textColor;
    public string ClosedMessage => _closedMessage;
}
