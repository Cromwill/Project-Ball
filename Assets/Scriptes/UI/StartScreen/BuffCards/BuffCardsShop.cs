using UnityEngine;
using UnityEngine.Advertisements;

public class BuffCardsShop : MonoBehaviour
{
    [SerializeField] private RewardedVideoAds _videoAds;

    private bool isFirstLaunch = true;
    private BuffCardProduct[] _buffCards;
    private BuffCardProduct _currentProduct;
    public void ClosePanel() => gameObject.SetActive(false);

    public void Launch()
    {
        if (_buffCards == null)
            _buffCards = GetComponentsInChildren<BuffCardProduct>();

        if (isFirstLaunch)
        {
            for (int i = 0; i < _buffCards.Length; i++)
            {
                _buffCards[i].Initialization();
                _buffCards[i].ProductChosed += ShowAds;
            }
        }
    }

    public void ConfirmCardReceipt(ShowResult result)
    {
        _videoAds.UnityAdsDidFinish -= ConfirmCardReceipt;
        _currentProduct.GetBuffCardToInventory();
        _currentProduct = null;
    }

    private void ShowAds(BuffCardProduct product)
    {
        _currentProduct = product;
        _videoAds.UnityAdsDidFinish += ConfirmCardReceipt;
        _videoAds.ShowRewardedVideo();
    }
}
