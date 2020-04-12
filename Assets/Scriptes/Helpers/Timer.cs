using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text _timeViewer;

    private void Start()
    {
        _timeViewer = GetComponent<Text>();
    }

    void Update()
    {
        _timeViewer.text = Time.timeSinceLevelLoad.ToString("0.##");
    }
}
