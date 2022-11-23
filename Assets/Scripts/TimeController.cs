using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] UIController _uiController;
    private void Start()
    {
        _uiController.OnTimeScaleChangedEventHandler += OnTimeScaleChanged;
    }

    private void Update()
    {

    }

    private void OnTimeScaleChanged(object sender, UIController.OnTimeScaleChangedEventArgs e)
    {
        #if UNITY_EDITOR
            Time.timeScale = 50; // Editor limit
        #else
            Time.timeScale = e.sliderValue;
        #endif
    }
}