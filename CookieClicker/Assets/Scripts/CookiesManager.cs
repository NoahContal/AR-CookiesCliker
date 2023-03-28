using UnityEngine;

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
        InvokeRepeating(nameof(AddCookies), 1, 1);
        InvokeRepeating(nameof(SpawnObject), 1, 1);
    }
    
    private void AddCookies()
    {
        cookies += cookiesPerSecond;
    }
    
    private void SpawnObject()
    {
        if (cookieCount >= 10) return;
        var position = new Vector3(
            Random.Range(-spawnRange.x, spawnRange.x),
            Random.Range(-spawnRange.y, spawnRange.y),
            Random.Range(-spawnRange.z, spawnRange.z));
        if (Camera.main != null) position += Camera.main.transform.position;
        var rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        Instantiate(objectsToSpawn, position, rotation, cookieParent.transform);
        cookieCount++;
    }
}
