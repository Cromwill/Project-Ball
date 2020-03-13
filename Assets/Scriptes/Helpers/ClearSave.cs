using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSave : MonoBehaviour
{
    public void OnClickButton()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
