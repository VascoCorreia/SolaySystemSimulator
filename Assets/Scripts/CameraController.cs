using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    //Publisher vai ser o meu dropdown
    //subsciber vai ser cameracontroller class

    //1 define a delegate - a contract between the publisher and the subscriber. It defines the signature of the method and the subscriber that is called
    //2 define an event based on the delegate
    //3 publish the event

    private Dropdown _planetDropdownMenu;

    void Start()
    {
        _planetDropdownMenu = GameObject.FindGameObjectWithTag("PlanetButton").GetComponent<Dropdown>();
    }

    // Update is called once per frames
    void Update()
    {
    }

    //Have a script in dropdown that references camera and moves it
    //have a script in cameracontroller that refences dropdown and moves it

    //funtion that depending on the option of the dropdown moves camera to that position
    
}
