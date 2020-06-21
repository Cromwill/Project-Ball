using System;
using UnityEngine;
using UnityEngine.Advertisements;


public class RewardedVideoAds : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private string _gameId;
    [SerializeField] private bool _isTestAds;
    [SerializeField] private string _noSkipAdsString;

    public event Action<ShowResult> UnityAdsDidFinish;

    public bool IsAdsReady => Advertisement.IsReady();

    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(_gameId, _isTestAds);
        }
        else
            Debug.Log("Platform is not supported");

    }

    public void ShowRewardedVideo(bool isCanSkipAds)
    {
        string skipAds = isCanSkipAds ? "video" : _noSkipAdsString;

        if (Advertisement.IsReady())
        {
            Advertisement.Show(skipAds);
        }
        else
        {
            OnUnityAdsDidFinish(skipAds, ShowResult.Failed);
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Ads did Error");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        UnityAdsDidFinish?.Invoke(showResult);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {

    }
}
