using UnityEngine;

public class ExitPanel : MonoBehaviour
{
    public void OnExitButtonClick()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
