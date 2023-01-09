using TMPro;
using UnityEngine;

public class PlanetNamesController : MonoBehaviour
{
    [SerializeField] Transform _parentPlanet;
    [SerializeField] Transform _mainCamera;
    [SerializeField] GameObject solarSystem;
    [SerializeField] float zAxisOffset;
    [SerializeField] float distanceFromCameraToDeactivate;
    [SerializeField] TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }
    void LateUpdate()
    {
        transform.position = new Vector3(_parentPlanet.position.x, _parentPlanet.position.y, _parentPlanet.position.z + zAxisOffset);
    }

    private void Update()
    {
        transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);

        if(Vector3.Distance(_mainCamera.transform.position, transform.position) < distanceFromCameraToDeactivate)
        {
            text.alpha = 0f;
        }
        else if (Vector3.Distance(_mainCamera.transform.position, transform.position) >= distanceFromCameraToDeactivate)
        {
            text.alpha = 1f;
        }
    }
}
