using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCookiePerClick;
    [SerializeField] private TextMeshProUGUI textCookieRate;
    [SerializeField] private TextMeshProUGUI textPassiveCookie;
    public TextMeshProUGUI cookieAmountDisplay;

    private CookiesManager _cookiesManager;
    
    [SerializeField] private Slider slider;

    private void Start()
    {
        _cookiesManager = GetComponent<CookiesManager>();
    }

    public void CookiePerTouch()
    {
        string text = textCookiePerClick.text;
        int price = int.Parse(text);
        if (_cookiesManager.cookies - price >= 0)
        {
            _cookiesManager.cookies -= price;
            
            price += 3;
            textCookiePerClick.text = price + "";
            
            _cookiesManager.cookiesPerTouch++;
        }
    }

    public void CookieRate()
    {
        string text = textCookieRate.text;
        int price = int.Parse(text);
        if (_cookiesManager.cookies - price >= 0)
        {
            _cookiesManager.cookies -= price;
            
            price += 3;
            textCookieRate.text = price + "";
            
            _cookiesManager.cookiesPerSecond++;
        }
    }

    private void Update()
    {
        cookieAmountDisplay.text = _cookiesManager.cookies + "";
        slider.value = _cookiesManager.cookies;

        if (_cookiesManager.cookies >= 1000000)
        {
            SceneManager.LoadScene("WinMenu");
        }
    }
}
