using UnityEngine;
using UnityEngine.UI;

public class PurchaseDataDrawer : MonoBehaviour
{
    [SerializeField] private Text[] _parameterNameFields;
    [SerializeField] private Text[] _valueFields;

    public void ShowData(string name, string value)
    {
        _parameterNameFields[0].text = name;
        _valueFields[0].text = value;
    }
}
