using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _zoomValue;
    //
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        float scrollValue = Input.mouseScrollDelta.y;

        if (scrollValue > 0)
        {
            _mainCamera.fieldOfView -= _zoomValue;
            ClampCameraFieldOfViewFiveBetweenSixty();
        }
        //
        else if (scrollValue < 0)
        {
            _mainCamera.fieldOfView += _zoomValue;
            ClampCameraFieldOfViewFiveBetweenSixty();
        }
    }

    private void ClampCameraFieldOfViewFiveBetweenSixty()
    {
        _mainCamera.fieldOfView = Mathf.Clamp(_mainCamera.fieldOfView, 5f, 60f);
    }

}
