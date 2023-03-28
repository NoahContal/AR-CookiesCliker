using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CookieKiller : MonoBehaviour
{
    [SerializeField] private CookiesManager cookiesManager;

    public GameObject particleEffect;
    private Vector2 _touchPosition;
    private RaycastHit _hit;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.touchCount <= 0) return;
        _touchPosition = Input.GetTouch(0).position;
        
        var ray = _camera.ScreenPointToRay(_touchPosition);
        if (Physics.Raycast(ray, out _hit))
        {
            var hitObject = _hit.transform.gameObject;
            if (hitObject.CompareTag("Cookie"))
            {
                var clone = Instantiate(particleEffect, hitObject.transform.position, Quaternion.identity);
                clone.transform.localScale = hitObject.transform.localScale;
                Destroy(hitObject);
                cookiesManager.cookieCount--;
                cookiesManager.cookies += cookiesManager.cookiesPerTouch;
                Destroy(clone, 1/cookiesManager.cookiesPerSecond);
            }
        }
    }
}
