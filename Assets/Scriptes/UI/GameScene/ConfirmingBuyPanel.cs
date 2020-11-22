using UnityEngine;
using UnityEngine.UI;

public class ConfirmingBuyPanel : MonoBehaviour
{
    [SerializeField] private Text _coast;
    [SerializeField] private ScoreFormConverter _scorreFormConverter;
    [SerializeField] private Animator _selfAnimator;
    [SerializeField] private GameObject _offerToViewAdsPanel;
    [SerializeField] private GameObject _confirmPanel;

    public void ShowCoast(float coast)
    {
        gameObject.SetActive(true);
        _coast.text = _scorreFormConverter.GetConvertedScore(coast);
    }

    public void PlayDangeringAnimation()
    {
        _selfAnimator.Play("NotEnoughMonyForBuyCard");
    }

    public void ShowOfferToViewAds()
    {
        _confirmPanel.SetActive(false);
        _offerToViewAdsPanel.SetActive(true);
    }
}
