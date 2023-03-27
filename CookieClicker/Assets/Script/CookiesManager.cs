using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CookiesManager : MonoBehaviour
{
    [SerializeField] private GameObject cookieGameObject;
    
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

    private void Update()
    {
        
    }
}
