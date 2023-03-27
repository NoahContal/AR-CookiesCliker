using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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
    
    [SerializeField] private GameObject[] objectsToSpawn; 
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private float spawnDelay = 2f;
    private float spawnTimer = 0f;
    
    private void Update()
    {
        spawnDelay = 2f / cookiesPerSecond;
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycastManager.Raycast(new Vector2(Screen.width / 2f, Screen.height / 2f), hits, TrackableType.PlaneWithinPolygon)) // si un plan est détecté
            {
                Pose hitPose = hits[0].pose;
                Vector3 spawnPosition = hitPose.position;
                Quaternion spawnRotation = hitPose.rotation;
                int randomIndex = Random.Range(0, objectsToSpawn.Length);
                GameObject newObject = Instantiate(objectsToSpawn[randomIndex], spawnPosition, spawnRotation);
                spawnTimer = 0f;
            }
        }
    }
}
