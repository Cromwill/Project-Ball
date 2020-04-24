using System;
using UnityEngine;
using UnityEngine.UI;

public class BuffCard : MonoBehaviour
{
    [SerializeField] private Text _scoreLableViewer;
    [SerializeField] private Text _timeLableViewer;
    private Button _selfButton;

    public event Action<BuffCard> CardSetting;

    public BuffCardData CardData { get; private set; }

    public void Filling(BuffCardData cardData)
    {
        CardData = cardData;
        _scoreLableViewer.text = CardData.ScoreModifierLable;
        _timeLableViewer.text = CardData.TimeModifierLable;
        _selfButton = GetComponent<Button>();

        _selfButton.onClick.AddListener(OnCardSetting);
    }

    public void OnCardSetting()
    {
        CardSetting?.Invoke(this);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
