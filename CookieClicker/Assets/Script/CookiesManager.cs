using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CookiesManager : MonoBehaviour
{
    public int cookies = 0;
    public int cookiesPerTouch = 1;
    public float cookiesPerSecond = 1;
    public int passiveCookiesPerSecond = 1;
    
    private void Start()
    {
        InvokeRepeating(nameof(AddCookies), 1, 1);
    }
    
    private void AddCookies()
    {
        cookies += passiveCookiesPerSecond;
    }

    private IEnumerator CookieLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/cookiesPerSecond);
            SpawnCookie();
        }
    }
    
    private void SpawnCookie()
    {
        
    }
}
