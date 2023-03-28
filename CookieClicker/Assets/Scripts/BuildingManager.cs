using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Building
{
    public GameObject Model;
    public Quaternion Rotation;
    public int BasePrice;
    public int ActualPrice;
    public int CookiePerTouch;
    public float CookiePerSecond;

    public string Name;
    public TextMeshProUGUI ButtonText;
    
    public Building(GameObject model, int basePrice, int cookiePerTouch, float cookiePerSecond, Quaternion rotation)
    {
        Model = model;
        BasePrice = basePrice;
        ActualPrice = basePrice;
        CookiePerTouch = cookiePerTouch;
        CookiePerSecond = cookiePerSecond;
        Rotation = rotation;
    }
    
    public void Upgrade()
    {
        ActualPrice = (int)(ActualPrice * 1.2f);
    }
    
    public void Display()
    {
        ButtonText.text = $"{Name} - {ActualPrice}";
    }
}

public class BuildingManager : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public TrackableType trackableType = TrackableType.PlaneWithinBounds;
    
    private Dictionary<string, Building> _buildings;
    private CookiesManager _cookiesManager;
    
    public GameObject cursor;
    public GameObject placeButton;
    public GameObject cancelButton;
    
    private bool _isPlacing;
    private GameObject _buildingToPlace;
    public Transform buildingParent;
    
    private void Start()
    {
        _buildings = new Dictionary<string, Building>();
        _buildings.Add("Milk", new Building(
            Resources.Load<GameObject>("3D Models/Milk/MilkFBX"),
            10, 1, 0.1f, new Quaternion(-90f, 0, 0, 0)));
        _buildings.Add("Blender", new Building(
            Resources.Load<GameObject>("3D Models/Blender/Mixer"),
            100, 10, 0.5f, new Quaternion()));
        _buildings.Add("Oven", new Building(
            Resources.Load<GameObject>("3D Models/Oven/Stove"),
            1000, 200, 1f, new Quaternion()));
        _buildings.Add("Farm", new Building(
            Resources.Load<GameObject>("3D Models/Farm/Farm"),
            25000, 5000, 3f, new Quaternion()));
        _buildings.Add("Factory", new Building(
            Resources.Load<GameObject>("3D Models/Factory/Factory"),
            500000, 25000, 10f, new Quaternion()));
        _cookiesManager = FindObjectOfType<CookiesManager>();
        foreach (var key in _buildings.Keys)
        {
            _buildings[key].ButtonText = GameObject.Find(key).GetComponentInChildren<TextMeshProUGUI>();
            _buildings[key].Name = key;
            _buildings[key].Display();
        }
        
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        cursor.SetActive(_isPlacing);
        placeButton.SetActive(_isPlacing);
        cancelButton.SetActive(_isPlacing);
    }
    
    public void StartBuildingPlacing(string buildingName)
    {
        var building = _buildings[buildingName];
        _isPlacing = true;
        UpdateUI();
        _buildingToPlace = Instantiate(building.Model, Vector3.zero, building.Rotation, buildingParent);
    }
    
    public void CancelPlacement()
    {
        _isPlacing = false;
        UpdateUI();
        Destroy(_buildingToPlace);
    }
}
