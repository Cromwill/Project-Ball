using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class LayoutGroupElementSize : MonoBehaviour
{
    private GridLayoutGroup _grid;
    private RectTransform _rectTransform;

    private void Start()
    {
        _grid = GetComponent<GridLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
        

        Debug.Log(Camera.main.rect);
    }
}
