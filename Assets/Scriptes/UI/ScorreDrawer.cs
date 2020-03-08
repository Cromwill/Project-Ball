using UnityEngine;
using UnityEngine.UI;

public class ScorreDrawer : MonoBehaviour
{
    [SerializeField] private Text _scorre;
    [SerializeField] private Text _scorreSpeed;
    [SerializeField] private string _format;

    public void Draw(int scorre)
    {
        _scorre.text = scorre.ToString();
    }

    public void DrawSpeed(float scorre)
    {
        _scorreSpeed.text = scorre.ToString(_format) + "point/sec";
    }
}
