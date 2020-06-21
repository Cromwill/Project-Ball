using UnityEngine;
using UnityEngine.UI;

public class ConfirmingBuyPanel : MonoBehaviour
{
    [SerializeField] private Text _coast;
    [SerializeField] private ScoreFormConverter _scorreFormConverter;
    [SerializeField] private Animator _selfAnimator;

    public void ShowCoast(float coast)
    {
        gameObject.SetActive(true);
        _coast.text = _scorreFormConverter.GetConvertedScore(coast);
    }

    public void PlayDangeringAnimation()
    {
        _selfAnimator.Play("NotEnoughMonyForBuyCard");
    }
}
