using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CelestialBody : MonoBehaviour
{
    [SerializeField] int id;

    [SerializeField] float tilt = 0, diameter = 1;

    [SerializeField] double mass;

    [SerializeField] float rotationTime;

    [SerializeField] Vector3 initialVelocity;

    [SerializeField] NetworkManager _networkManager;

    GravitySimulation gravitySimulation;

    Vector3d currentVelocityDouble;
    Vector3d positionDouble;
    private void Awake()
    {
        gravitySimulation = new GravitySimulation();
    }
    void Start()
    {

        TrailRenderer trailRenderer = gameObject.AddComponent<TrailRenderer>();

        trailRenderer.material = new Material(Shader.Find("Sprites/Default"));
        trailRenderer.startWidth = 0.2f;
        trailRenderer.endWidth = 0.01f;
        trailRenderer.time = 1f;
        trailRenderer.startColor = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
        trailRenderer.sortingLayerName = "Foreground";

        currentVelocityDouble = new Vector3d(initialVelocity);
        positionDouble = new Vector3d(transform.localPosition);

        //InvokeRepeating("GetText", 1, 2);
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    public void NewtonianGravitation(CelestialBody[] bodyToPull)
    {
        foreach (CelestialBody body in bodyToPull)
        {
            if (body != this)
            {
                double distanceBetweenBodiesSqr = (body.positionDouble - positionDouble).sqrMagnitude;
                Vector3 directionBetweenBodies = (body.transform.position - transform.position).normalized;

                double force = ((gravitySimulation.gravitationalConstant * (mass * body.mass)) / distanceBetweenBodiesSqr);
                Vector3d acceleration = new Vector3d(directionBetweenBodies) * (force / mass);

                currentVelocityDouble += acceleration * (Time.fixedDeltaTime * UnitScaling.fixedGameTime);

            }
        }
    }
    public void applyGravity()
    {
        positionDouble += currentVelocityDouble * (Time.fixedDeltaTime * UnitScaling.fixedGameTime);
        transform.localPosition = new Vector3((float)positionDouble.x, (float)positionDouble.y, (float)positionDouble.z);

    }

    void Rotation()
    {
        //360 degrees divided by total rotation time = degrees rotated per second
        double rotation = (360d / rotationTime) * (Time.fixedDeltaTime * UnitScaling.fixedGameTime);
        transform.Rotate(0, (float)rotation, 0);
    }

    //void updatePlanetMass()
    //{
    //    CelestialBodyData data = ApiRequests.getPlanetData(id);
    //    _networkManager.getPlanetData(id);
    //    mass = data.mass;
    //}

}
