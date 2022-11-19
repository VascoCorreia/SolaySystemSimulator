using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownEvent : MonoBehaviour
{
    TMP_Dropdown _dropdown;

    //This delegate/"method" will be the event handler in the subscribers
    public delegate void DropdownPlanetClickedEventHandler(object source, int value); //this arguments are .NET convention. source is the class that that is publishing the event, and args is any data that we want to send in the event
    
    public event DropdownPlanetClickedEventHandler planetClicked;

    // Start is called before the first frame update
    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();

        _dropdown.onValueChanged.AddListener(delegate {
            onPlanetClicked(_dropdown);
        });
    }

    //this method will raise the event and notify subscribers
    protected virtual void onPlanetClicked(TMP_Dropdown dropdown)
    {
        if (planetClicked != null)
        {
            //this class is publishing the event
            planetClicked(this, _dropdown.value);
        }
    }   
}
