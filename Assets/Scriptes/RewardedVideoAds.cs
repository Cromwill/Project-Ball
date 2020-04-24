using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedVideoAds : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private string _gameId;

    public event Action<ShowResult> UnityAdsDidFinish;

    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(_gameId, true);
        }
        else
            Debug.Log("Platform is not supported");
    }

    public void ShowRewardedVideo()
    {
        if (Advertisement.IsReady())
            Advertisement.Show();
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Ads did Error");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
            UnityAdsDidFinish?.Invoke(showResult);
        else if (showResult == ShowResult.Failed)
            Debug.Log("ads did finish with failed");
        else if (showResult == ShowResult.Skipped)
            UnityAdsDidFinish?.Invoke(showResult);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {

    }
}
