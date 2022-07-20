using System.Collections;
using UnityEngine;
using TMPro;

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager Instance { get; private set; }
    //
    private Camera _mainCamera;
    private RaycastHit _hit;
    private bool _isUIPanelActive;
    [SerializeField] private TMP_Text _planetNameText;
    //
    private float _firstWaitTimeForPlanetInfo = 0f;

    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
        //
        _mainCamera = Camera.main;
    }

    void Start()
    {
        StartCoroutine(WaitFirstThreeSecondsForPlanetInfo());
    }

    IEnumerator WaitFirstThreeSecondsForPlanetInfo()
    {
        yield return new WaitForSeconds(3f);
        _firstWaitTimeForPlanetInfo = 3.1f;
    }


    public void GetPlanetInfoToUIPanel(string planetName)
    {
        if (_firstWaitTimeForPlanetInfo < 3f) return;
        //
        else if (_isUIPanelActive) return;
        //
        else if (!Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out _hit)) return;
        //
        else if (planetName != _hit.transform.name) return;
        //
        _planetNameText.text = _hit.transform.name;
        _planetNameText.transform.parent.gameObject.SetActive(true);
        GameManager.Instance.StopCameraToRotateAroundSound();
        _isUIPanelActive = true;
        
    }

    public void CloseUIPanel()
    {
        _planetNameText.transform.parent.gameObject.SetActive(false);
        GameManager.Instance.SetCameraToRotateAroundSound();
        _isUIPanelActive = false;
    }
}
