using System;
using UnityEngine;
using UnityEngine.UI;

public class BuffCardProduct : MonoBehaviour
{
    [SerializeField] private BuffCardData _selfCardData;
    [SerializeField] private float _sleepTime;

    public event Action<BuffCardProduct> ProductChosed;

    private Button _selfButton;
    private float _stopTime;

    public void Initialization()
    {
        _selfButton = GetComponentInChildren<Button>();
        _selfButton.onClick.AddListener(OnProductChosed);
    }

    public void GetBuffCardToInventory()
    {
        _selfCardData.SaveCard();
    }

    private void OnProductChosed()
    {
        ProductChosed?.Invoke(this);
    }
}
