using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private float _targetAspect;
    [Range(0f, 1f)]
    [SerializeField] private float _widthOrHeight = 0;

    private Camera _camera;

    private float _initialSize;


    private void Start()
    {
        _camera = GetComponent<Camera>();
        _initialSize = _camera.orthographicSize;
    }

    private void Update()
    {
        if (_camera.orthographic)
        {
            float constantWidthSize = _initialSize * (_targetAspect / _camera.aspect);
            _camera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, _widthOrHeight);
        }
    }
}
