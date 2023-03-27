using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCookiePerClick;
    [SerializeField] private TextMeshProUGUI textCookieRate;
    [SerializeField] private TextMeshProUGUI textPassiveCookie;
    [SerializeField] private TextMeshProUGUI Cookies;

    private CookiesManager _cookiesManager;
    
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

    public void PassiveCookie()
    {
        string text = textPassiveCookie.text;
        int price = int.Parse(text);
        if (_cookiesManager.cookies - price >= 0)
        {
            _cookiesManager.cookies -= price;
            
            price += 3;
            textPassiveCookie.text = price + "";
            
            _cookiesManager.passiveCookiesPerSecond++;
        }
    }

    private void Update()
    {
        Cookies.text = _cookiesManager.cookies + "";
    }
}
