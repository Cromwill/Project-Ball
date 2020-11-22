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
    public bool IsRewardedReady => _isUnityPlayMod? true : Appodeal.isLoaded(Appodeal.REWARDED_VIDEO);
    public bool IsInterstitialReady => _isUnityPlayMod ? true : Appodeal.canShow(Appodeal.INTERSTITIAL);
    private void Start()
    {
        Initialize(_isTestAds);
    }

    public void ShowRewardedVideo(bool isCanSkipAds)
    {
        if(_isUnityPlayMod)
        {
            UnityAdsDidFinish?.Invoke();
        }
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
    }

    public void ShowInterstitial()
    {
        if (_isUnityPlayMod)
        {
            UnityAdsDidFinish?.Invoke();
        }

        if (Appodeal.canShow(Appodeal.INTERSTITIAL, _selectedObjectAtShopPlacement))
        {
            Appodeal.show(Appodeal.INTERSTITIAL, _selectedObjectAtShopPlacement);
        }
    }
    #region INTERSTITIAL
    public void onInterstitialLoaded(bool isPrecache)
    {
        
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
        
    }

    public void onInterstitialClicked()
    {
        
    }

    public void onInterstitialExpired()
    {
        
    }

    #endregion

    #region RewardedVideo
    public void onRewardedVideoLoaded(bool precache)
    {
        
    }

    public void onRewardedVideoFailedToLoad()
    {
        
    }

    public void onRewardedVideoShowFailed()
    {
       
    }

    public void onRewardedVideoShown()
    {
        UnityAdsDidFinish?.Invoke();
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        UnityAdsDidFinish?.Invoke();
    }

    public void onRewardedVideoClosed(bool finished)
    {
        
    }

    public void onRewardedVideoExpired()
    {
        
    }

    public void onRewardedVideoClicked()
    {
        
    }

    #endregion

    private void Initialize(bool isTesting)
    {
        Appodeal.setLogLevel(Appodeal.LogLevel.Verbose);
        Appodeal.setTesting(isTesting);
        Appodeal.setInterstitialCallbacks(this);
        Appodeal.setRewardedVideoCallbacks(this);
        Appodeal.muteVideosIfCallsMuted(true);
        Appodeal.initialize(_gameId, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);
    }
}
