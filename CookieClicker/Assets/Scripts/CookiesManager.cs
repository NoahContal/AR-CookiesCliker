using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CookiesManager : MonoBehaviour
{
    private GameObject _cookieGameObject;
    
    public int cookies = 0;
    public int cookiesPerTouch = 1;
    public int cookiesPerSecond = 1;
    
    [SerializeField] private GameObject cookieParent;
    [SerializeField] private GameObject objectsToSpawn;
    [SerializeField] private Vector3 spawnRange = new Vector3 (1,1,1);
    public int cookieCount = 0;

    private void Start()
    {
        StartCoroutine(nameof(SpawnObject));
    }
    
    private void AddCookies()
    {
        cookies += cookiesPerSecond;
    }
    
    private IEnumerator SpawnObject()
    {
        while (true)
        {
            if (cookieCount < 30)
            {
                var position = new Vector3(
                    Random.Range(-spawnRange.x, spawnRange.x),
                    Random.Range(-spawnRange.y, spawnRange.y),
                    Random.Range(-spawnRange.z, spawnRange.z));
                Debug.Log(position);
                var rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                var spawnedObject = Instantiate(objectsToSpawn, position, rotation, cookieParent.transform);
                cookieCount++;
            }
            yield return new WaitForSeconds(1/cookiesPerSecond);
        }
    }
}
