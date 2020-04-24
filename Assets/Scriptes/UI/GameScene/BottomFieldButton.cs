using UnityEngine;
using UnityEngine.UI;

public class BottomFieldButton : MonoBehaviour
{
    [SerializeField] private GameObject _selfPanel;
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] private Sprite _inselectedSprite;

    private Image _selfImage;

    public void OpenPanel()
    {
        if (_selfImage == null)
            Initializing();

        _selfPanel.SetActive(true);
        _selfImage.sprite = _selectedSprite;
    }

    public void ClosePanel()
    {
        if (_selfImage == null)
            Initializing();

        _selfPanel.SetActive(false);
        _selfImage.sprite = _inselectedSprite;
    }

    private void Initializing() => _selfImage = GetComponent<Image>();
}
