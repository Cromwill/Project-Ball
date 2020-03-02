using UnityEngine;

public class BottomFieldButton : MonoBehaviour
{
    [SerializeField] private GameObject _selfPanel;
    [SerializeField] private int _priority;

    public bool IsOpen => _selfPanel.active;

    public void OpenPanel()
    {
        _selfPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        _selfPanel.SetActive(false);
    }
}
