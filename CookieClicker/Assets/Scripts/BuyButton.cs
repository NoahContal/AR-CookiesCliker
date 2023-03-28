using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    private CookiesManager _cookiesManager;
    public int price;
    private bool _isAffordable;
    private Button _button;
    
    private void Start()
    {
        _cookiesManager = FindObjectOfType<CookiesManager>();
        _button = GetComponent<Button>();
        _button.interactable = false;
    }
    
    private void Update()
    {
        var affordable = _cookiesManager.cookies >= price;
        if (affordable == _isAffordable) return;
        _isAffordable = affordable;
        _button.interactable = _isAffordable;
    }
}
