using UnityEngine;
using UnityEngine.UI;

public class LevelDrawer : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _midleground;
    [SerializeField] private string _levelName;

    private void Start()
    {
        SetBackgroundColor(GameDataStorage.GetColorForLevel(_levelName, ColorPlace.Background));
        SetMidlegroundColor(GameDataStorage.GetColorForLevel(_levelName, ColorPlace.MidleGround));
    }

    public void SetBackgroundColor(Color color)
    {
        SetColor(_background, color);
    }

    public void SetMidlegroundColor(Color color)
    {
        SetColor(_midleground, color);
    }

    private void SetColor(Image image, Color color)
    {
        image.color = color;
    }
}
