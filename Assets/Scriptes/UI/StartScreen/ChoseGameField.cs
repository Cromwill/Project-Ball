using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelsShop))]
public class ChoseGameField : MonoBehaviour
{
    [SerializeField] private GameField[] _gameFields;
    [SerializeField] private RewardedVideoAds _videoAds;

    private bool _canLoadLevel;
    private int _currentLevelIndex;
    public GameField[] GameFields => _gameFields;

    private void Start()
    {
        for(int i = 0; i < _gameFields.Length; i++)
        {
            _gameFields[i].SetChoseGameField(this);
        }
    }
    public string[] GetOpenLevelNames()
    {
        return _gameFields.Where(a => a.IsOpenLevel).Select(a => a.LevelName).ToArray();
    }

    public void LevelPlay(int levelIndex)
    {
        _currentLevelIndex = levelIndex;
        _canLoadLevel = true;
        _videoAds.UnityAdsDidFinish += LevelLoad;
        _videoAds.ShowRewardedVideo();
    }

    private void LevelLoad(ShowResult result)
    {
        if(_canLoadLevel && _currentLevelIndex != 0)
        {
            if (result == ShowResult.Finished || result == ShowResult.Skipped)
            {
                _videoAds.UnityAdsDidFinish -= LevelLoad;
                _canLoadLevel = false;
                SceneManager.LoadScene(_currentLevelIndex, LoadSceneMode.Single);
            }
        }
    }
}
