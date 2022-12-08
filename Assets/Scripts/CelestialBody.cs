using System.Collections.Generic;
using UnityEngine;

//5065452 mercury
//21021120 venus
//86148 Earth rotation time in seconds (regular time)
//88632 marte
//35712 jupiter
//36828 saturno
//62028 urano
//57780 neptuno
//552060 plutao

public class CelestialBody : MonoBehaviour
{
    [SerializeField]
    float tilt = 0, diameter = 1, mass, gravitationalConstant = 6.674e-10f;

    [SerializeField]
    int rotationTime = 500000;

    [SerializeField]
    Vector3 initialVelocity;

    Vector3 currentVelocity;

    void Start()
    {
        transform.Rotate(0, 0, tilt);
        currentVelocity = initialVelocity;
    }

    void FixedUpdate()
    {
        Rotation();
        applyGravity();
    }
    void Rotation()
    {
        //360 degrees divided by total rotation time = degrees rotated per second
        float rotation = ((360f / rotationTime) * Time.fixedDeltaTime);
        transform.Rotate(0, rotation, 0);
    }

    //need to move this function somewhere else
    //this function gives me the force F=ma
    public void NewtonianGravitation(List<CelestialBody> bodyToPull)
    {
        foreach (CelestialBody body in bodyToPull)
        {
            if (body != this)
            {
                float distanceBetweenBodiesSqr = (body.transform.position - transform.position).sqrMagnitude;

                Vector3 directionBetweenBodies = (body.transform.position - transform.position).normalized;
                Vector3 force = directionBetweenBodies * gravitationalConstant * ((mass * body.mass) / distanceBetweenBodiesSqr);
                Vector3 acceleration = force / mass;

                currentVelocity += acceleration * (Time.fixedDeltaTime * UnitScaling.fixedGameTime);
            }
        }
    }

    public void applyGravity()
    {
        transform.position += currentVelocity * (Time.fixedDeltaTime * UnitScaling.fixedGameTime);
    }
}

