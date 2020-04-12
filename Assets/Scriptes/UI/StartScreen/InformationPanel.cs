using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    [SerializeField] private Text _timeAwayValue;
    [SerializeField] private Text _cashEarnedValue;

    private Button _closeButton;
    public void Show(string timeAway, string cashEarned)
    {
        _closeButton = GetComponentInChildren<Button>();
        if (_closeButton.onClick == null)
            _closeButton.onClick?.AddListener(Close);

        _timeAwayValue.text = timeAway;
        _cashEarnedValue.text = cashEarned;
    }

    public void Close() => gameObject.SetActive(false);
}
