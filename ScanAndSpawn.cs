using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI; 

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class ARLevelController : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private List<GameObject> levelPrefabs;
    [SerializeField] private float autoPlaceDistance = 1.5f;
    [SerializeField] private Text levelText; 

    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject currentLevel;
    private int currentLevelIndex = 0;
    private bool isLevelPlaced = false;

    private Vector3 lastLevelPosition;
    private Quaternion lastLevelRotation;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    void Start()
    {
        StartCoroutine(InitializeLevel());
    }

    void Update()
    {
        if (!isLevelPlaced && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                TryPlaceLevel();
            }
        }
    }

    private IEnumerator InitializeLevel()
    {
        yield return new WaitForSeconds(0.5f);
        LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        CleanupPreviousLevel();
        EnablePlaneDetection();
        isLevelPlaced = false;
        UpdateLevelText(); 
    }

    private void TryPlaceLevel()
    {
        if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            PlaceLevel(hits[0].pose.position);
        }
        else
        {
            PlaceLevelFallback();
        }
    }

    private void PlaceLevel(Vector3 position, Quaternion rotation = default)
    {
        if (rotation == default)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            rotation = Quaternion.LookRotation(cameraForward.normalized);
        }

        currentLevel = Instantiate(
            levelPrefabs[currentLevelIndex],
            position,
            rotation
        );

        lastLevelPosition = position;
        lastLevelRotation = rotation;

        FinalizePlacement();
    }

    private void PlaceLevelFallback()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 spawnPosition = Camera.main.transform.position +
                               cameraForward * autoPlaceDistance;

        currentLevel = Instantiate(
            levelPrefabs[currentLevelIndex],
            spawnPosition,
            Quaternion.LookRotation(cameraForward)
        );

        FinalizePlacement();
    }

    private void FinalizePlacement()
    {
        isLevelPlaced = true;
        DisablePlaneDetection();

        var finishTrigger = currentLevel.GetComponentInChildren<FinishTrigger>();
        if (finishTrigger != null)
        {
            finishTrigger.Initialize(this);
        }
    }

    public void RestartLevel()
    {
        CleanupPreviousLevel();
        LoadCurrentLevel();
    }

    public void CompleteLevel()
    {
        StartCoroutine(TransitionToNextLevel());
    }

    private IEnumerator TransitionToNextLevel()
    {
        yield return new WaitForSeconds(0.3f);
        currentLevelIndex = (currentLevelIndex + 1) % levelPrefabs.Count;
        LoadCurrentLevel();
    }

    private void CleanupPreviousLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
            currentLevel = null;
        }
    }

    private void EnablePlaneDetection()
    {
        arPlaneManager.enabled = true;
        SetPlanesActive(true);
    }

    private void DisablePlaneDetection()
    {
        arPlaneManager.enabled = false;
        SetPlanesActive(false);
    }

    private void SetPlanesActive(bool state)
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(state);
        }
    }

    public void LoadLevelByIndex(int index)
    {
        if (index < 0 || index >= levelPrefabs.Count) return;

        currentLevelIndex = index;
        LoadCurrentLevel();
    }

    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = (currentLevelIndex + 1).ToString();
        }
    }

    public int CurrentLevelIndex => currentLevelIndex;
}
