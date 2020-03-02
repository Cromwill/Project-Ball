using UnityEngine;

public class BottomFieldButton : MonoBehaviour
{
    [SerializeField] private GameObject _selfPanel;

    public void OpenPanel()
    {
        _selfPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        _selfPanel.SetActive(false);
    }
}
