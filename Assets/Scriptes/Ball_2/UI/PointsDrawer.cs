using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsDrawer : MonoBehaviour
{
    [SerializeField] private Text _points;

    public void Draw(int points)
    {
        _points.text = points.ToString();
    }
}
