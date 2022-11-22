using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] List<GameObject> planetList = new List<GameObject>();

    void Start()
    {
        //class that controls dropdown planet event
        DropdownEvent _planetDropdownMenu;

        _planetDropdownMenu = GameObject.FindGameObjectWithTag("PlanetButton").GetComponent<DropdownEvent>();

        if (_planetDropdownMenu != null)
            _planetDropdownMenu.OnDropdownPlanetClickedEventHandler += OnPlanetClicked;
    }

    void Update()
    {
    }

    //we need to subscribe to the event
    void OnPlanetClicked(object sender, DropdownEvent.OnPlanetClickedEventArgs planetDropdownMenuData)
    {
        switch (planetDropdownMenuData.planetIndex)
        {
            //Should i parent the camera to the planet?
            //How to choose the distance from the planet to the camera?


            case 0: //move to mercury
                Debug.Log("Moving to mercury");
                break;

            case 1://move to venus
                Debug.Log("Moving to venus");
                break;
            case 2: //move to earth
                Debug.Log("Moving to Earth");
                GetComponent<Transform>().position = new Vector3(planetList[2].transform.position.x, planetList[2].transform.position.y, planetList[2].transform.position.z);
                break;
                //    case 3: //move to mars
                //        Debug.Log("Moving to mars");
                //        break;
                //    case 4: //move to jupiter
                //        Debug.Log("Moving to jupiter");
                //        break;
                //    case 5: //move to saturn
                //        Debug.Log("Moving to saturn");
                //        break;
                //    case 6: //move to uranus
                //        Debug.Log("Moving to uranus");
                //        break;
                //    case 7: //move to neptune
                //        Debug.Log("Moving to neptune");
                //        break;

                //    default: //move to solar system view
                //        break;
                //}
        }
    }
}
