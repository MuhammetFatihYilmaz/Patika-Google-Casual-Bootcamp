using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    [SerializeField] private Transform _sunTransform;
    [SerializeField] private float _rotateSpeedAroundSun;
    [SerializeField] private float _rotateSpeedOwnAxis;
    private Vector3 _startPos;
    private float _distance;
    private bool _isLogged;

    void Start()
    {
        _startPos = transform.position;
        StartCoroutine(RotatePlanetWithCoroutine());
    }

    IEnumerator RotatePlanetWithCoroutine()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            transform.RotateAround(_sunTransform.position, Vector3.up, _rotateSpeedAroundSun * Time.deltaTime);
            transform.Rotate(Vector3.up * _rotateSpeedOwnAxis * Time.deltaTime);
            //
            _distance = Vector3.Distance(transform.position, _startPos);

            if (_distance > 1 && !_isLogged)
                continue;

            else if (_distance > 1 && _isLogged)
                _isLogged = false;
            //
            if (_distance > 0 && _distance < 0.1f)
                WriteLog();
        }
        
    }

    private void WriteLog()
    {
        if(!_isLogged)
        {
            Debug.Log($"Complete {gameObject.name}",gameObject);
            _isLogged = true;
        }
    }
}
