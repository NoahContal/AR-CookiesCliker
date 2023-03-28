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
    private Button _placeButton;
    public GameObject cancelButton;
    public GameObject noPlaneText;
    
    private bool _isPlacing = false;
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
            100, 10, 5));
        _buildings.Add("Oven", new Building(
            Resources.Load<GameObject>("3D Models/Oven/Stove"),
            1000, 200, 20));
        _buildings.Add("Farm", new Building(
            Resources.Load<GameObject>("3D Models/Farm/Farm"),
            25000, 5000, 100));
        _buildings.Add("Factory", new Building(
            Resources.Load<GameObject>("3D Models/Factory/Factory"),
            500000, 25000, 1000));
        _cookiesManager = FindObjectOfType<CookiesManager>();
        foreach (var key in _buildings.Keys)
        {
            Debug.Log(key);
            var button = GameObject.Find(key);
            _buildings[key].Button = button.GetComponent<BuyButton>();
            _buildings[key].ButtonText = button.GetComponentInChildren<TextMeshProUGUI>();
            button.GetComponent<Button>().onClick.AddListener(() => StartBuildingPlacing(key));
            _buildings[key].Name = key;
            _buildings[key].Display();
        }
        _placeButton = placeButton.GetComponent<Button>();
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

    private readonly List<ARRaycastHit> _hits = new();
    private void Update()
    {
        if (!_isPlacing) return;
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        
        if (raycastManager.Raycast(screenCenter, _hits, trackableType))
        {
            var hitPose = _hits[0].pose;
            _buildingToPlace.transform.position = hitPose.position;
            _placeButton.interactable = true;
            if (noPlaneText.activeSelf)
                noPlaneText.SetActive(false);
        }
        else
        {
            _placeButton.interactable = false;
            if (!noPlaneText.activeSelf)
                noPlaneText.SetActive(true);
        }
    }
}
