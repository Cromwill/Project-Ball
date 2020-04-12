using UnityEngine;

[CreateAssetMenu(fileName = "new ScorreForm", menuName = "Create ScorreForm")]
public class ScorreFormConverter : ScriptableObject
{
    [SerializeField] private string _scorrePerSecondEnding;
    [SerializeField] private string[] _scorreEnding;
    [SerializeField] private string _stringFormat;

    private const int _scorrePowerStep = 3;

    public string GetConvertedScorre(float scorre)
    {
        float convertingValue = scorre;
        string convertedValue = "";

        if (scorre > 1000)
        {
            for (int i = 1; i < _scorreEnding.Length; i++)
            {
                convertingValue = scorre / Mathf.Pow(10, _scorrePowerStep * i);
                if (convertingValue >= 1 && convertingValue < 1000)
                {
                    convertedValue = convertingValue.ToString(_stringFormat) + _scorreEnding[i];
                }
            }
        }
        else
        {
            convertedValue = ((int)convertingValue).ToString() + _scorreEnding[0];
        }

        return convertedValue;
    }

    public string GetConvertedScorrePerSecond(float value)
    {
        return value.ToString(_stringFormat) + _scorrePerSecondEnding;
    }
}
