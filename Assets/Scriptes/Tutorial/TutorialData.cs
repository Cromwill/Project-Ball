using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new TutorialData", menuName ="TutorialData")]
public class TutorialData : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] private string[] _messages;

    public string GetMessage(int index) => _messages[index];
    public bool IsCanContinue(int index) => index < _messages.Length;
}
