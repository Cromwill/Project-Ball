using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdsTest : MonoBehaviour
{
    [SerializeField] private Text _initValue;
    [SerializeField] private Text _initCountText;
    [SerializeField] private Text _rewardedValue;
    [SerializeField] private Text _rewardedCountText;

    private RewardedVideoAds _commertial;

    private int _initCount;
    private int _rewardedCount;

    private void Start()
    {
        _commertial = FindObjectOfType<RewardedVideoAds>();
        _initCount = 0;
        _rewardedCount = 0;

        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        while(true)
        {
            _initValue.text = _commertial.IsInterstitialReady.ToString();
            _rewardedValue.text = _commertial.IsRewardedReady.ToString();

            _initCount++;
            _rewardedCount++;

            _initCountText.text = _initCount.ToString();
            _rewardedCountText.text = _rewardedCount.ToString();


            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ShowRewarded()
    {
        _commertial.ShowRewardedVideo(false);
    }

    public void Showinit()
    {
        _commertial.ShowInterstitial();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
}
