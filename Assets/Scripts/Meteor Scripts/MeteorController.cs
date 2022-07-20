using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MeteorController : MonoBehaviour
{
    private Rigidbody _meteorRigidbody;
    private TrailRenderer _trailRenderer;
    //
    [SerializeField] private float _meteorFollowSpeed;
    [SerializeField] private float _meteorThrowSpeed;
    [SerializeField] private GameObject[] _planets;
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private ParticleSystem _meteorBurningParticle;
    private int randomIndex;
    //
    private float _destroyTime = 0;

    private void Awake()
    {
        _meteorRigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _planets = GameManager.Instance.Planets;
    }

    private void Start()
    {
        randomIndex = Random.Range(0, 4);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GetMeteorKeepFollowToPlanetStatus())
        {
            MeteorFollowingToPlanet();
            return;
        }
        else
            ThrowMeteorToPlanet();
    }

    private void MeteorFollowingToPlanet()
    {
        transform.position = Vector3.MoveTowards(transform.position, _planets[randomIndex].transform.position, _meteorFollowSpeed * Time.deltaTime);
    }

    private void ThrowMeteorToPlanet()
    {
        Vector3 _distance = _planets[randomIndex].transform.position - transform.position;
        _meteorRigidbody.AddForce(_distance.normalized * _meteorThrowSpeed);
        _destroyTime += Time.deltaTime;
        if(_destroyTime>3f)
        {
            if (_explosionParticle.transform.parent != transform)
                _explosionParticle.transform.parent = transform;
            //
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        _meteorBurningParticle.Stop();
        _meteorBurningParticle.Clear();
        _trailRenderer.enabled = false;
        _explosionParticle.transform.position = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        _explosionParticle.transform.parent = other.gameObject.transform;
        _explosionParticle.gameObject.SetActive(true);
        _explosionParticle.Play();
        StartCoroutine(StopExplosionCoroutine());
    }

    IEnumerator StopExplosionCoroutine()
    {
        yield return new WaitForSeconds(2);
        _explosionParticle.transform.parent = transform;
        Destroy(gameObject);
    }
}
