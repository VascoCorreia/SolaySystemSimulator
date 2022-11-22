using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [SerializeField]
    float tilt, diameter, mass;
    [SerializeField]
    int rotationTime;

    //5065452 mercury
    //21021120 venus
    //86148 Earth rotation time in seconds (regular time)
    //88632 marte
    //35712 jupiter
    //36828 saturno
    //62028 urano
    //57780 neptuno
    //552060 plutao


    void Rotation()
    {
        transform.Rotate(0, (360 / diameter) * Time.deltaTime, 0);
    }
    // Start is called before the first frame update
    void Start()
    {

        transform.Rotate(0, 0, tilt);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       Rotation();
    }
}
