using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown _dropdown;

    [SerializeField]
    TMP_Text _timeScale;

    [SerializeField]
    Slider _timeScaleSlider;

    [SerializeField]
    TMP_Text _currTimeStep;

    //This delegate/"method" will be the event handler in the subscribers
    //this arguments are .NET convention. source is the class that that is publishing the event, and args is any data that we want to send in the event
    public event EventHandler<OnPlanetClickedEventArgs> OnDropdownPlanetClickedEventHandler;
    public event EventHandler<OnTimeScaleChangedEventArgs> OnTimeScaleChangedEventHandler;

    //data to be sent on event
    public class OnPlanetClickedEventArgs : EventArgs
    {
        public int planetIndex;
    }
    public class OnTimeScaleChangedEventArgs : EventArgs
    {
        public int sliderValue;
        public TMP_Text currTimeStep;

    }

    void Start()
    {
        planetDropDownMenuController();
        timeScaleSliderController();
    }

    //this method will raise the event and notify subscribers
    protected virtual void OnPlanetClicked(TMP_Dropdown dropdown)
    {
        OnDropdownPlanetClickedEventHandler?.Invoke(this, new OnPlanetClickedEventArgs { planetIndex = dropdown.value });
    }

    protected virtual void OnTimeScaleChanged(Slider slider)
    {
        OnTimeScaleChangedEventHandler?.Invoke(this, new OnTimeScaleChangedEventArgs { sliderValue = (int)slider.value, currTimeStep = _currTimeStep });
    }

    void planetDropDownMenuController()
    {

        _dropdown?.onValueChanged.AddListener(delegate { OnPlanetClicked(_dropdown); });
    }

    void timeScaleSliderController()
    {
        _timeScaleSlider?.onValueChanged.AddListener(delegate { OnTimeScaleChanged(_timeScaleSlider); });
    }
}
