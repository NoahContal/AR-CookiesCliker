using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    private CookiesManager cookiesManager;
    public int price;
    private bool _isAffordable;
    private Button _button;
    
    private void Start()
    {
        cookiesManager = FindObjectOfType<CookiesManager>();
        _button = GetComponent<Button>();
        _button.interactable = false;
    }
    
    private void Update()
    {
        var affordable = cookiesManager.cookies >= price;
        if (affordable == _isAffordable) return;
        Debug.Log($"Affordable: {affordable}");
        _isAffordable = affordable;
        _button.interactable = _isAffordable;
    }
}
