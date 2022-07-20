using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //
    private bool _isCameraRotateAroundSun;
    private bool _isPlanetPanelOpen;
    //
    [SerializeField] private Toggle _keepFollowPlanetToggle;
    [SerializeField] private GameObject[] _planets;
    public GameObject[] Planets { get { return _planets; } private set { _planets = value; } }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    public void SetCameraToRotateAroundSound()
    {
        if(_isPlanetPanelOpen)
            _isPlanetPanelOpen = false;
        //
            StartCoroutine(TimeDelayForRotatingCamera());
    }

    IEnumerator TimeDelayForRotatingCamera()
    {
        yield return new WaitForSeconds(3);
        if (!_isPlanetPanelOpen)
            _isCameraRotateAroundSun = true;
    }

    public void StopCameraToRotateAroundSound()
    {
        StopCoroutine(TimeDelayForRotatingCamera());
        _isCameraRotateAroundSun = false;
        _isPlanetPanelOpen = true;
    }

    public bool GetCameraRotateStatus()
    {
        return _isCameraRotateAroundSun;
    }

    public bool GetMeteorKeepFollowToPlanetStatus()
    {
        if (_keepFollowPlanetToggle.isOn)
            return true;
        else
            return false;
    }
}
