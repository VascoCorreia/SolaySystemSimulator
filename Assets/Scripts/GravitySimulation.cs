using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravitySimulation : MonoBehaviour
{
    List<CelestialBody> celestialBodies = new List<CelestialBody>();
    void Awake()
    {
        celestialBodies = FindObjectsOfType<CelestialBody>().ToList();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < celestialBodies.Count; i++)
        {
            celestialBodies[i].NewtonianGravitation(celestialBodies);
        }

        for (int i = 0; i < celestialBodies.Count; i++)
        {
            celestialBodies[i].applyGravity();
        }
    }


    //foreach (CelestialBody c in celestialBodies)
    //{
    //    if (c != this)
    //    {
    //        rb.AddForce(NewtonianGravitation(c));
    //    }
    //}
}
