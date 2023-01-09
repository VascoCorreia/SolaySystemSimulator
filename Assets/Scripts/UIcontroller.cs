using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Dropdown _dropdown;
    [SerializeField] TMP_Text _timeScale;
    [SerializeField] Slider _timeScaleSlider;
    [SerializeField] TMP_Text _currTimeStep;
    [SerializeField] TMP_Text[] _uiPlanetData;
    [SerializeField] CameraController _camera;
    [SerializeField] GameObject _planetDataUI;
    [SerializeField] NetworkManager _networkManager;
    [SerializeField] Button resetBtn;
    [SerializeField] RectTransform[] _planetDataUIText;

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
        resetBtn.onClick.AddListener(_networkManager.resetGame);
    }

    private void FixedUpdate()
    {
        deactivatePlanetDataUI();
    }
    //this method will raise the event and notify subscribers
    protected virtual void OnPlanetClicked(TMP_Dropdown dropdown)
    {
        activatePlanetDataUI();
        updatePlanetUIData(dropdown.value, _uiPlanetData);
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

    void updatePlanetUIData(int id, TMP_Text[] data)
    {
        CelestialBodyData planetData = ApiRequests.getPlanetData(id);

        //name
        data[0].text = planetData.name;

        //mass
        data[1].text = "Mass: " + planetData.mass.ToString() + " Kg";

        //diameter
        data[2].text = "Diameter: " + (planetData.diameter / 1000).ToString() + " Km";

        //tilt
        data[3].text = "Axial Tilt: " + planetData.tilt.ToString() + "º";

        //Orbital period (s)
        data[4].text = "Orbital Period: " + (planetData.rotationTime / 3600).ToString() + " hours";

        //velocity
        data[5].text = "Velocity: " + planetData.initialVelocity.ToString() + " m/s";
    }

    void deactivatePlanetDataUI()
    {
        if (!_camera.isLocked || _dropdown.value == 0)
        {
            LeanTween.alpha(_planetDataUI.GetComponent<RectTransform>(), 0, 1);
            LeanTween.moveLocalX(_planetDataUI, 1500, 1);
        }
    }

    void activatePlanetDataUI()
    {
        LeanTween.alpha(_planetDataUI.GetComponent<RectTransform>(), 0.211f, 1);
        LeanTween.moveLocal(_planetDataUI, new Vector3(940, -1.7f), 1);
    }

    public void resetScene()
    {

        UnitScaling.fixedGameTime = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
