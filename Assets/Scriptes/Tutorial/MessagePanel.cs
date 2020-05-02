using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MessagePanel : MonoBehaviour
{
    [SerializeField] private TutorialData _tutorialData;
    private Text _textMessage;
    private Animator _selfAnimator;
    private int _tutorialNumber;

    public event Action TutorialFinished;

    public void ShowMessage()
    {
        gameObject.SetActive(true);
        if (_textMessage == null)
            Initialization();

        _textMessage.text = _tutorialData.GetMessage(GetMessageIndex());
        _selfAnimator.Play("OpenTutorialPanel");
    }

    public void NextMessage()
    {
        gameObject.SetActive(false);
        if (_tutorialData.IsCanContinue(_tutorialNumber))
            ShowMessage();
        else
        {
            TutorialFinished?.Invoke();
        }
    }

    private int GetMessageIndex()
    {
        int value = _tutorialNumber;
        _tutorialNumber++;
        return value;
    }

    private void Initialization()
    {
        _selfAnimator = GetComponent<Animator>();
        _textMessage = GetComponentInChildren<Text>();
        _tutorialNumber = 0;
    }
}
