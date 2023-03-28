using System.Collections;
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
        StartCoroutine(nameof(SpawnObject));
    }
    
    private void AddCookies()
    {
        cookies += cookiesPerSecond;
    }
    
    [SerializeField] private GameObject objectsToSpawn;
    [SerializeField] private Vector3 spawnRaange;
    
    private IEnumerator SpawnObject()
    {
        var position = new Vector3(
            Random.Range(-spawnRaange.x, spawnRaange.x), 
            Random.Range(-spawnRaange.y, spawnRaange.y), 
            Random.Range(-spawnRaange.z, spawnRaange.z));
        var rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        var spawnedObject = Instantiate(objectsToSpawn, position, rotation);
        spawnedObject.transform.parent = transform;
        yield return new WaitForSeconds(1/cookiesPerSecond);
    }
}
