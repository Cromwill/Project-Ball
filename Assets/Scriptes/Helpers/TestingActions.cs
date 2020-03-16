using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingActions : MonoBehaviour
{
    [SerializeField] private LevelsFieldScorre _scorre;

    public void DeletedSaves()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void AddScorre(int value)
    {
        _scorre.AddScorre(value);
    }

    public void ReduseScorre(int value)
    {
        _scorre.ReductionScorre(value);
    }
}
