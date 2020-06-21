using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

class GameFieldComponentsHelper
{
    public Button SelfButton { get; set; }
    public GameFieldSeller Seller { get; set; }
    public Image SelfPanel { get; set; }
    public Animator SelfAnimator { get; set; }
    public AudioSource AudioSours { get; set; }
    public ChoseGameField Chooser { get; set; }
    public LevelsFieldScore ScorePanel { get; set; }


    public bool IsCanOpenLevel()
    {
        if (Seller.isCanBuy(Chooser.GetScoreSum()))
        {
            SelfAnimator.Play("OpeningGamePanel");
            return true;
        }
        else
            return false;
    }

    public void OpenGameField(UnityAction action, Sprite openPanelSprite)
    {
        Seller.CloseSeller();
        SelfButton.interactable = true;
        SelfButton.onClick.AddListener(action);
        SelfPanel.sprite = openPanelSprite;
    }
}