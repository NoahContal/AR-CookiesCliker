using UnityEngine;

public class CookiesManager : MonoBehaviour
{
    private GameObject _cookieGameObject;
    
    public int cookies = 0;
    public int cookiesPerTouch = 1;
    public int cookiesPerSecond = 1;
    
    private void Start()
    {
        InvokeRepeating(nameof(AddCookies), 1, 1);
    }
    
    private void AddCookies()
    {
        cookies += cookiesPerSecond;
    }

    private void Update()
    {
        
    }
}
