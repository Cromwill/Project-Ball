using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] private Text _lableViewer;
    [SerializeField] private Text _parameterNameViewer;
    [SerializeField] private Text _parameterValueViewer;

    public void Show(string lable, string parameterName, string parametrValue)
    {
        _lableViewer.text = lable;
        _parameterNameViewer.text = parameterName;
        _parameterValueViewer.text = parametrValue;
    }

    public void OnExitButtonClick()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
