using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonInteractableChecker : MonoBehaviour
{
    [SerializeField] private bool _isCanScipCommercial;

    private Button _selfButton;
    private RewardedVideoAds _commercial;

    private void Start()
    {
        _selfButton = GetComponent<Button>();
        _commercial = FindObjectOfType<RewardedVideoAds>();

        StartCoroutine(InteractableChecker(_isCanScipCommercial));
    }


    private IEnumerator InteractableChecker(bool isCanScip)
    {
        while (true)
        {
            if (_selfButton != null && _commercial != null)
            {
                bool commercialReady = isCanScip ? _commercial.IsInterstitialReady : _commercial.IsRewardedReady;
                _selfButton.interactable = commercialReady;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
