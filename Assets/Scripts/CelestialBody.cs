using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [SerializeField]
    float tilt, diameter, mass;
    [SerializeField]
    int rotationTime;
    [SerializeField]
    GameObject parentStar;

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
        float rotation = (360 / 5) * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 0, tilt);
    }

    // Update is called once per frame
    void Update()
    {
       Rotation();
    }
}
