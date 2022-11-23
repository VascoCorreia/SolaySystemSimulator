//using System;
//using TMPro;
//using UnityEngine;

//public class DropdownEvent : MonoBehaviour
//{

//    //This delegate/"method" will be the event handler in the subscribers
//    //this arguments are .NET convention. source is the class that that is publishing the event, and args is any data that we want to send in the event
//    public event EventHandler<OnPlanetClickedEventArgs> OnDropdownPlanetClickedEventHandler;

//    //data to be sent on event
//    public class OnPlanetClickedEventArgs : EventArgs
//    {
//        public int planetIndex;
//    }

//    void Start()
//    {
//        TMP_Dropdown _dropdown;
//        _dropdown = GetComponent<TMP_Dropdown>();

//        if (_dropdown != null)
//            _dropdown.onValueChanged.AddListener(delegate { OnPlanetClicked(_dropdown); });
//    }

//    //this method will raise the event and notify subscribers
//    protected virtual void OnPlanetClicked(TMP_Dropdown dropdown)
//    {
//        OnDropdownPlanetClickedEventHandler?.Invoke(this, new OnPlanetClickedEventArgs { planetIndex = dropdown.value });
//    }
//}
