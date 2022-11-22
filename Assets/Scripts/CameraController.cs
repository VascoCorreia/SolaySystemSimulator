using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    

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
    private void OnPlanetClicked(object sender, DropdownEvent.OnPlanetClickedEventArgs e)
    {
        Debug.Log(e.planetIndex);
        //switch (dropdown.value) //option on the dropdown
        //{

        //    //send an event when changing that lets camera know (subscriber) that needs to move to the correct planet
        //    case 0: //move to mercury
        //        Debug.Log("Moving to mercury");
        //        break;
        //    case 1://move to venus
        //        Debug.Log("Moving to venus");
        //        break;
        //    case 2: //move to earth
        //        Debug.Log("Moving to earth");
        //        break;
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
