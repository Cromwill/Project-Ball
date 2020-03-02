using UnityEngine;
using UnityEngine.UI;

public class ScorreDrawer : MonoBehaviour
{
    [SerializeField] private Text _scorre;

    public void Draw(int scorre)
    {
        _scorre.text = scorre.ToString();
    }
}
