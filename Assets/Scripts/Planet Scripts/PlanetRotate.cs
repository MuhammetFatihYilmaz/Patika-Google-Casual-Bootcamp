using System.Collections;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    [SerializeField] private float _rotateSpeedOwnAxis;
    [SerializeField] private float _rotateSpeedAroundSun;
    [SerializeField] private Transform[] _rotatePointsParent;
    //
    private int _runningRotatePointsParentNo = 0;

    private void Start()
    {
        //Start planet rotation sequence around own axis.
        StartCoroutine(RotatePlanetAroundOwnAxis());
        //Start planet rotation sequence around Sun.
        StartCoroutine(RotatePlanetAroundSunWithBezierCurve());
    }

    IEnumerator RotatePlanetAroundOwnAxis()
    {
        while(true)
        {
            //Rotate planet around own axis.
            transform.Rotate(Vector3.up * _rotateSpeedOwnAxis * Time.deltaTime);
            //Wait for end of frame.
            yield return new WaitForEndOfFrame();
        }
    }

    //Rotate planet around sun with Bezier Curve.
    IEnumerator RotatePlanetAroundSunWithBezierCurve()
    {
        Vector3 positionToGo = transform.position;
        float t = 0f;
        while(t<1)
        {
            t += Time.deltaTime * _rotateSpeedAroundSun;
            positionToGo = BezierCurveManager.Instance.GetBezierCurvePoint(t, _rotatePointsParent[_runningRotatePointsParentNo].GetChild(0).position,
            _rotatePointsParent[_runningRotatePointsParentNo].GetChild(1).position, _rotatePointsParent[_runningRotatePointsParentNo].GetChild(2).position,
            _rotatePointsParent[_runningRotatePointsParentNo].GetChild(3).position);
            transform.position = positionToGo;
            yield return new WaitForEndOfFrame();
        }

        _runningRotatePointsParentNo++;

        if(_runningRotatePointsParentNo >= _rotatePointsParent.Length)
        {
            _runningRotatePointsParentNo = 0;
            Debug.Log($"{gameObject.name} has completed rotating around Sun.", gameObject);
        }

        StartCoroutine(RotatePlanetAroundSunWithBezierCurve());
        yield break;
    }
}
