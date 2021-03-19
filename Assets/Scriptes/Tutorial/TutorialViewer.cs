using System.Collections;
using UnityEngine;

public class TutorialViewer : MonoBehaviour
{
    [SerializeField] private MessagePanel _messagePanel;
    [SerializeField] private string _saveKey;

    private void Start()
    {
        _messagePanel.TutorialFinished += OnTutorialFinished;
        if (!PlayerPrefs.HasKey(_saveKey))
            StartCoroutine(FirstShowTutorialPanel());
    }

    private void OnTutorialFinished()
    {
        PlayerPrefs.SetInt(_saveKey, 1);
        FindObjectOfType<FBHelper>().LogCompletedTutorialEvent("tutorial", true);
    }

    private IEnumerator FirstShowTutorialPanel()
    {
        yield return new WaitForSeconds(1.5f);
        _messagePanel.ShowMessage();
    }
}