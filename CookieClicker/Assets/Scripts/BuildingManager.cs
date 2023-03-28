using System;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Building
{
    public GameObject Model;
    public int BasePrice;
    public int ActualPrice;
    public int CookiesPerTouch;
    public int CookiesPerSecond;

    public string Name;
    public BuyButton Button;
    public TextMeshProUGUI ButtonText;
    
    public Building(GameObject model, int basePrice, int cookiePerTouch, int cookiePerSecond)
    {
        Model = model;
        BasePrice = basePrice;
        ActualPrice = basePrice;
        CookiesPerTouch = cookiePerTouch;
        CookiesPerSecond = cookiePerSecond;
    }
    
    public void Upgrade()
    {
        ActualPrice = (int)(ActualPrice * 1.2f);
        Button.price = ActualPrice;
        Display();
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
    private Building _buildingToPlaceInfo;
    private GameObject _buildingToPlace;
    public Transform buildingParent;
    
    private void Start()
    {
        _buildings = new Dictionary<string, Building>();
        _buildings.Add("Milk", new Building(
            Resources.Load<GameObject>("3D Models/Milk/MilkFBX"),
            10, 1, 1));
        _buildings.Add("Blender", new Building(
            Resources.Load<GameObject>("3D Models/Blender/Mixer"),
            100, 10, 2));
        _buildings.Add("Oven", new Building(
            Resources.Load<GameObject>("3D Models/Oven/Stove"),
            1000, 200, 3));
        _buildings.Add("Farm", new Building(
            Resources.Load<GameObject>("3D Models/Farm/Farm"),
            25000, 5000, 5));
        _buildings.Add("Factory", new Building(
            Resources.Load<GameObject>("3D Models/Factory/Factory"),
            500000, 25000, 10));
        _cookiesManager = FindObjectOfType<CookiesManager>();
        foreach (var key in _buildings.Keys)
        {
            var button = GameObject.Find(key);
            _buildings[key].Button = button.GetComponent<BuyButton>();
            _buildings[key].ButtonText = button.GetComponentInChildren<TextMeshProUGUI>();
            button.GetComponent<Button>().onClick.AddListener(() => StartBuildingPlacing(key));
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

    private void StartBuildingPlacing(string buildingName)
    {
        if (_isPlacing) return;
        var building = _buildings[buildingName];
        _buildingToPlaceInfo = building;
        _isPlacing = true;
        UpdateUI();
        _buildingToPlace = Instantiate(building.Model, buildingParent);
    }
    
    public void CancelPlacement()
    {
        _isPlacing = false;
        UpdateUI();
        Destroy(_buildingToPlace);
    }
    
    public void PlaceBuilding()
    {
        _isPlacing = false;
        UpdateUI();
        var building = _buildingToPlaceInfo;
        _cookiesManager.cookies -= building.ActualPrice;
        _cookiesManager.cookiesPerSecond += building.CookiesPerSecond;
        _cookiesManager.cookiesPerTouch += building.CookiesPerTouch;
        building.Upgrade();
        _buildingToPlace = null;
    }

    private void Update()
    {
        if (!_isPlacing) return;
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, trackableType);
        if (hits.Count <= 0) return;
        var hitPose = hits[0].pose;
        _buildingToPlace.transform.position = hitPose.position;
    }
}
