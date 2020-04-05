using UnityEngine;
using UnityEngine.UI;

public class ConfirmingBuyPanel : MonoBehaviour
{
    [SerializeField] private Text _coast;
    [SerializeField] private ScorreFormConverter _scorreFormConverter;
    [SerializeField] private Animator _selfAnimator;

    public void ShowCoast(float coast)
    {
        _coast.text = _scorreFormConverter.GetConvertedScorre(coast);
    }

    public void PlayDangeringAnimation()
    {
        _selfAnimator.Play("NotEnoughMonyForBuyCard");
    }
}
