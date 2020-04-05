using UnityEngine;
using UnityEngine.UI;

public class ScorreDrawer : MonoBehaviour
{
    [SerializeField] private Text _scorre;
    [SerializeField] private Text _scorreSpeed;
    [SerializeField] private string _format;

    public void Draw(string value )
    {
        _scorre.text = value;
    }

    public void DrawSpeed(string scorre)
    {
        _scorreSpeed.text = scorre;
    }
}
