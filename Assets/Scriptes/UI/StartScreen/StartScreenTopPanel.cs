using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class StartScreenTopPanel : MonoBehaviour
{
    [SerializeField] private BuffCardsShop _buffCardsShop;
    [SerializeField] private RewardedVideoAds _videoAds;
    [SerializeField] private float _watchAdsTimeSleep;
    [SerializeField] private Button _watchAdsButton;
    [SerializeField] private StartScreenScoreCounter _scoreCounter;

    private float _currenWatchAdsTime;

    private void Start()
    {
        _watchAdsButton.interactable = true;
        _currenWatchAdsTime = 0;
    }

    private void Update()
    {
        _currenWatchAdsTime -= Time.deltaTime;

        _watchAdsButton.interactable = _currenWatchAdsTime < 0;
    }

    public void DeletedSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void OpenBuffCardsShop()
    {
        _buffCardsShop.gameObject.SetActive(true);
        _buffCardsShop.Launch();
    }

    public void StartAds()
    {
        _videoAds.UnityAdsDidFinish += AdsResult;
        _videoAds.ShowRewardedVideo(false);
    }

    private void AdsResult(ShowResult result)
    {
        _videoAds.UnityAdsDidFinish -= AdsResult;

        if (result == ShowResult.Finished)
        {
            _scoreCounter.AddScoreAllFields(900);
            _currenWatchAdsTime = _watchAdsTimeSleep;
        }
    }
}
