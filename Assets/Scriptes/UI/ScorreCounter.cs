using UnityEngine;

public class ScorreCounter : MonoBehaviour
{
    [SerializeField] ScorreDrawer _pointsDrawer;

    private int _scorre;

    public void AddingScorre(int scorre)
    {
        ChangeScorre(scorre);
    }

    public void ReductionScorre(int scorre)
    {
        ChangeScorre(scorre * -1);
    }

    private void ChangeScorre(int scorre)
    {
        _scorre += scorre;
        _pointsDrawer.Draw(_scorre);
    }
}
