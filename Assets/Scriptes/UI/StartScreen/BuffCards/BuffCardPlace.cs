using UnityEngine;
using UnityEngine.UI;

public class BuffCardPlace : MonoBehaviour
{

    [SerializeField] private Text _scoreViewer;
    [SerializeField] private Text _timeViewer;
    [SerializeField] private Sprite _openCard;
    [SerializeField] private Sprite _closeCard;

    private Image _image;

    public bool IsEmptyPlace { get; private set; }

    public void SetCard(BuffCardData cardData)
    {
        if (_image == null)
            _image = GetComponent<Image>();

        _image.sprite = _openCard;
        _scoreViewer.text = cardData.ScoreModifierLable;
        _timeViewer.text = cardData.TimeModifierLable;
        IsEmptyPlace = false;
    }

    public void Initialization(BuffCardData cardData)
    {
        if (cardData == null)
            IsEmptyPlace = true;
        else
            SetCard(cardData);
    }
}
