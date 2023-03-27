using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Scene2Manager : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public TrackableType typeToTrack = TrackableType.PlaneWithinBounds;
    public GameObject prefabToInstantiate;

    private void OnTouch()
    {
        var touch = Input.GetTouch(0);
        List<ARRaycastHit> hits = new();
        raycastManager.Raycast(touch.position, hits, typeToTrack);

        if (hits.Count > 0)
        {
            var firstHit = hits[0];
            InstantiateObject(firstHit.pose.position, firstHit.pose.rotation);
        }
    }
    
    private void InstantiateObject(Vector3 position, Quaternion rotation)
    {
        Instantiate(prefabToInstantiate, position, rotation);
    }

    private void Update()
    {
        if (Input.touchCount > 0) OnTouch();
        
    }
}
