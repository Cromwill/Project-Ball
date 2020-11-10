using System;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;


public class RewardedVideoAds : MonoBehaviour, IInterstitialAdListener, IRewardedVideoAdListener
{
    [SerializeField] private string _gameId;
    [SerializeField] private bool _isTestAds;
    [SerializeField] private bool _isUnityPlayMod;
    [SerializeField] private string _selectedObjectAtShopPlacement;

    public event Action UnityAdsDidFinish;
    public bool IsRewardedReady => Appodeal.isLoaded(Appodeal.REWARDED_VIDEO);
    public bool IsInterstitialReady => Appodeal.isLoaded(Appodeal.INTERSTITIAL);
    private void Start()
    {
        Initialize(_isTestAds);
    }

    public void ShowRewardedVideo(bool isCanSkipAds)
    {
        int skipAds = isCanSkipAds ? Appodeal.INTERSTITIAL : Appodeal.REWARDED_VIDEO;

        if (Appodeal.isLoaded(skipAds))
        {
            Appodeal.show(skipAds);
        }
    }

    public void ShowInterstitial()
    {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
        {
            Debug.Log("Appodeal is loaded");
            Appodeal.show(Appodeal.INTERSTITIAL, _selectedObjectAtShopPlacement);
        }
    }
    #region INTERSTITIAL
    public void onInterstitialLoaded(bool isPrecache)
    {
        throw new NotImplementedException();
    }

    public void onInterstitialFailedToLoad()
    {
        UnityAdsDidFinish?.Invoke();
    }

    public void onInterstitialShowFailed()
    {
        UnityAdsDidFinish?.Invoke();
    }

    public void onInterstitialShown()
    {
        UnityAdsDidFinish?.Invoke();
    }

    public void onInterstitialClosed()
    {
        throw new NotImplementedException();
    }

    public void onInterstitialClicked()
    {
        throw new NotImplementedException();
    }

    public void onInterstitialExpired()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region RewardedVideo
    public void onRewardedVideoLoaded(bool precache)
    {
        throw new NotImplementedException();
    }

    public void onRewardedVideoFailedToLoad()
    {
        throw new NotImplementedException();
    }

    public void onRewardedVideoShowFailed()
    {
        throw new NotImplementedException();
    }

    public void onRewardedVideoShown()
    {
        Debug.Log("video shown");
        UnityAdsDidFinish?.Invoke();
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        Debug.Log("video finished");
        UnityAdsDidFinish?.Invoke();
    }

    public void onRewardedVideoClosed(bool finished)
    {
        throw new NotImplementedException();
    }

    public void onRewardedVideoExpired()
    {
        throw new NotImplementedException();
    }

    public void onRewardedVideoClicked()
    {
        throw new NotImplementedException();
    }

    #endregion

    private void Initialize(bool isTesting)
    {
        Appodeal.setTesting(isTesting);

        Appodeal.setInterstitialCallbacks(this);
        Appodeal.setRewardedVideoCallbacks(this);
        Appodeal.muteVideosIfCallsMuted(true);
        Appodeal.initialize(_gameId, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);
    }
}
