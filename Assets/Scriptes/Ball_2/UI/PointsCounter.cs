using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] PointsDrawer _pointsDrawer;

    private int _points;

    public void AddingPoints(int point)
    {
        ChangePoints(point);
    }

    public void ReductionPoints(int point)
    {
        ChangePoints(point * -1);
    }

    private void ChangePoints(int point)
    {
        _points += point;
        _pointsDrawer.Draw(_points);
    }
}
