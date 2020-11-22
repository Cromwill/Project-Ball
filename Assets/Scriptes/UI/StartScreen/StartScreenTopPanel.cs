using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenTopPanel : MonoBehaviour
{
    [SerializeField] private RewardedVideoAds _videoAds;
    [SerializeField] private float _watchAdsTimeSleep;
    [SerializeField] private Button _watchAdsButton;
    [SerializeField] private StartScreenScoreCounter _scoreCounter;

    private float _currenWatchAdsTime;

    private void Start()
    {
        _watchAdsButton.interactable = _videoAds.IsRewardedReady;
        _currenWatchAdsTime = 0;
    }

    private void Update()
    {
        _currenWatchAdsTime -= Time.deltaTime;
        AdsButtonInteractableCheck();
    }

    public void DeletedSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void LoadTestScene()
    {
        SceneManager.LoadScene(7);
    }

    public void StartAds(int addSecond)
    {
        _videoAds.UnityAdsDidFinish += AdsResult;
        _videoAds.ShowRewardedVideo(false);
    }

    private void AdsResult()
    {
        _videoAds.UnityAdsDidFinish -= AdsResult;
        _scoreCounter.AddScoreAllFields(300);
        _currenWatchAdsTime = _watchAdsTimeSleep;
    }

    private void AdsButtonInteractableCheck()
    {
        //_watchAdsButton.interactable = _currenWatchAdsTime < 0;
        _watchAdsButton.interactable = _videoAds.IsRewardedReady;
    }
}
