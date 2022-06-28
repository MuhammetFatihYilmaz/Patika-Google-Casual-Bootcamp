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
        //Keeping start position.
        _startPos = transform.position;
        //Start planet rotation sequence.
        StartCoroutine(RotatePlanetWithCoroutine());
    }

    IEnumerator RotatePlanetWithCoroutine()
    {
        while(true)
        {
            //Wait for end of frame.
            yield return new WaitForEndOfFrame();
            //Rotate planet around sun.
            transform.RotateAround(_sunTransform.position, Vector3.up, _rotateSpeedAroundSun * Time.deltaTime);
            //Rotate planet around own axis.
            transform.Rotate(Vector3.up * _rotateSpeedOwnAxis * Time.deltaTime);
            //Calculate  distance between starting position and current position.
            _distance = Vector3.Distance(transform.position, _startPos);

            //If distance greater than one and if not logged that rotate complete, then continue.
            if (_distance > 1 && !_isLogged)
                continue;
            //Else if distance greater than one and if logged that rotate complete, then _isLogged set false.
            else if (_distance > 1 && _isLogged)
                _isLogged = false;
            //If distance greater than zero and distance less than point one, then log that rotate complete.
            if (_distance > 0 && _distance < 0.1f)
                WriteLog();
        }
        
    }

    private void WriteLog()
    {
        if(!_isLogged)
        {
            //Log that rotate complete.
            Debug.Log($"Complete {gameObject.name}",gameObject);
            //_isLogged set true.
            _isLogged = true;
        }
    }
}
