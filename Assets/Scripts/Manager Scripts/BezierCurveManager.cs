using UnityEngine;

public class BezierCurveManager : MonoBehaviour
{
    public static BezierCurveManager Instance { get; private set; }
    //
    [SerializeField] private Transform[] _curvePoints;
    [Range(0.02f, 0.1f)] [SerializeField] private float _tSpeed = 0.05f;

    private Vector3 _gizmoPosition;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    private void OnDrawGizmos()
    {
        for (float t = 0; t < 1; t += _tSpeed)
        {
            Gizmos.color = Color.red;
            _gizmoPosition = GetBezierCurvePoint(t, _curvePoints[0].position, _curvePoints[1].position, _curvePoints[2].position, _curvePoints[3].position);
            Gizmos.DrawSphere(_gizmoPosition, 0.25f);
        }
    }

    public Vector3 GetBezierCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Mathf.Pow(1 - t, 3) * p0 +
                         3 * Mathf.Pow(1 - t, 2) * t * p1 +
                         3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
                         Mathf.Pow(t, 3) * p3;
    }
}
