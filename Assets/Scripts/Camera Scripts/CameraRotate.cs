using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private Transform _sunTransform;
    [SerializeField] private float _startingRotateSpeedAroundSun;
    [SerializeField] private float _fasterRotateSpeedAroundSun;
    //
    private RaycastHit _hit;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        GameManager.Instance.SetCameraToRotateAroundSound();
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.GetCameraRotateStatus())
            transform.RotateAround(_sunTransform.position, Vector3.up, _startingRotateSpeedAroundSun * Time.deltaTime);
        //
        if (Input.GetMouseButton(0))
            transform.RotateAround(_sunTransform.transform.position, Vector3.up, Input.GetAxis("Mouse X") * _fasterRotateSpeedAroundSun);
    }
}
