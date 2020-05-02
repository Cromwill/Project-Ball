using UnityEngine;

[CreateAssetMenu(fileName = "new BuffCard", menuName = "Create BuffCard")]
public class BuffCardData : ScriptableObject
{
    [SerializeField] private int _scoreModifierValue;
    [SerializeField] private int _timeModifierValue;
    [SerializeField] private Sprite OpenCard;
    [SerializeField] private Sprite CloseCard;

    const int secondInMinute = 60;

    public string ScoreModifierLable => ("scorre \n" + _scoreModifierValue.ToString()).ToUpper();
    public int ScoreModifierValue => _scoreModifierValue;
    public string TimeModifierLable => ((_timeModifierValue / secondInMinute) + "min").ToUpper();
    public int TimeModifierValue => _timeModifierValue;

    public string NameCard => ScoreModifierLable + "_" + TimeModifierLable;

    public void SaveCard()
    {
        string key = "Card_" + ScoreModifierLable + "_" + TimeModifierLable;
        if(PlayerPrefs.HasKey(key))
        {
            int cardCount = PlayerPrefs.GetInt(key);
            PlayerPrefs.SetInt(key, ++cardCount);
        }
        else
        {
            PlayerPrefs.SetInt(key, 1);
        }
    }

    public int GetCount()
    {
        string key = "Card_" + ScoreModifierLable + "_" + TimeModifierLable;
        return PlayerPrefs.GetInt(key);
    }
}
