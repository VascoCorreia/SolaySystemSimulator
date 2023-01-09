using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] GameObject[] _planetList;
    [SerializeField] UIController _uiController;
    [SerializeField] Vector3 _cameraSolarSystemDefaultRotation;
    [SerializeField] Vector3 _cameraSolarSystemDefaultPosition;
    [SerializeField] GameObject solarSystem;

    float _horizontalRotation = 0f, _verticalRotation = 0f;

    public bool isLocked { get; set; }

    Vector3 destination;
    private void OnValidate()
    {
        ChangeCameraRotationInEditor(_cameraSolarSystemDefaultRotation);
        ChangeCameraPositionInEditor(_cameraSolarSystemDefaultPosition);
    }

    void Start()
    {
        if (_uiController != null)
        {
            _uiController.OnDropdownPlanetClickedEventHandler += OnPlanetClicked;
            //_uiController.OnDropdownPlanetClickedEventHandler += ApiRequests.getPlanetData;

        }


        isLocked = true;
    }

    private void FixedUpdate()
    {
        CameraClickAndRotate();
        CameraMoveWithScrollWheel();
        cameraMovementWithKeys();
        updateCameraPositionWhenPlanetClicked();
        updateCameraRotation();
        enableOrDisableTrailRenderer(isLocked, _planetList);

    }

    //we need to subscribe to the event
    void OnPlanetClicked(object sender, UIController.OnPlanetClickedEventArgs planetDropdownMenuData)
    {
        switch (planetDropdownMenuData.planetIndex)
        {
            case 0: //move to solar system view
                transform.parent = solarSystem.transform;
                isLocked = true;
                break;

            case 1: //move to Sun
                transform.parent = _planetList[8].transform;
                isLocked = true;
                break;

            case 2: //move to mercury
                Debug.Log("Moving to mercury");
                transform.parent = _planetList[0].transform;
                isLocked = true;
                break;

            case 3://move to venus
                Debug.Log("Moving to venus");
                transform.parent = _planetList[1].transform;
                isLocked = true;
                break;

            case 4: //move to earth
                Debug.Log("Moving to Earth");
                transform.parent = _planetList[2].transform;
                isLocked = true;
                break;

            case 5: //move to mars
                Debug.Log("Moving to mars");
                transform.parent = _planetList[3].transform;
                isLocked = true;
                break;

            case 6: //move to jupiter
                Debug.Log("Moving to jupiter");
                transform.parent = _planetList[4].transform;
                isLocked = true;
                break;

            case 7: //move to saturn
                Debug.Log("Moving to saturn");
                transform.parent = _planetList[5].transform;
                isLocked = true;
                break;

            case 8: //move to uranus
                Debug.Log("Moving to uranus");
                transform.parent = _planetList[6].transform;
                isLocked = true;
                break;

            case 9: //move to neptune
                Debug.Log("Moving to neptune");
                transform.parent = _planetList[7].transform;
                isLocked = true;
                break;

            default:
                break;

        }
    }

    void CameraClickAndRotate()
    {
        int _horizontalRotationSpeed = 180, _verticalRotationSpeed = 180;

        if (Input.GetMouseButton(1))
        {
            if (transform.parent == solarSystem.transform && isLocked)
            {
                _horizontalRotation = _cameraSolarSystemDefaultRotation.y;
                _verticalRotation = _cameraSolarSystemDefaultRotation.x;

            }
            else if (transform.parent != solarSystem.transform)
            {
                _horizontalRotation = 0;
                _verticalRotation = 0;
            }

            _horizontalRotation += _horizontalRotationSpeed * Input.GetAxis("Mouse X") * Time.fixedDeltaTime;
            _verticalRotation -= _verticalRotationSpeed * Input.GetAxis("Mouse Y") * Time.fixedDeltaTime;

            transform.eulerAngles = new Vector3(_verticalRotation, _horizontalRotation, transform.rotation.z);

            isLocked = false;
            transform.parent = solarSystem.transform;
        }
    }


    void CameraMoveWithScrollWheel()
    {
        int _cameraMovementSpeed = 300;
        if (Input.GetMouseButton(1))
        {
            isLocked = false;
            transform.parent = solarSystem.transform;
            Vector2 mousePos = Input.mousePosition;
            Vector3 mousePositionInWorldSpace = _mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _mainCamera.nearClipPlane + 1));
            Vector3 directionOfMovement = (mousePositionInWorldSpace - _mainCamera.transform.position).normalized;

            transform.position += directionOfMovement * _cameraMovementSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.fixedDeltaTime;
        }
    }

    void cameraMovementWithKeys()
    {
        float speed = 30.0f;
        Vector3 keyInput = GetBaseInput();

        if (keyInput.sqrMagnitude > 0)
        {
            keyInput = keyInput * speed * Time.fixedDeltaTime;

            transform.Translate(keyInput);
        }
    }

    void updateCameraPositionWhenPlanetClicked()
    {
        //if (transform.parent == GameObject.Find("SolarSystem").transform)
        //{
        //    destination = _cameraSolarSystemDefaultPosition;
        //}        
        //Vector3 destionations = new Vector3((float)destination.x, (float)destination.y, (float)destination.z);
        //Vector3d currentPositionDouble = new Vector3d(transform.localPosition);

        //Vector3d positionDouble = Vector3d.Lerp(currentPositionDouble, destination, _cameraLerpSpeed);

        //transform.localPosition = new Vector3((float)positionDouble.x, (float)positionDouble.y, (float)positionDouble.z);
        //if(transform.localPosition.x < 0.001 && transform.localPosition.y < 0.001)
        //{
        //    transform.localPosition = new Vector3(0,0, -_mainCamera.nearClipPlane * 1.5f);
        //}
        if (isLocked)
        {
            if (transform.parent == solarSystem.transform)
            {
                transform.localPosition = _cameraSolarSystemDefaultPosition;
            }
            else
            {
                destination = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z - (_mainCamera.nearClipPlane * 1.5f));

                transform.position = destination;
            }
        }
    }

    void updateCameraRotation()
    {
        if (isLocked)
        {
            if (transform.parent == solarSystem.transform)
            {
                transform.localRotation = Quaternion.Euler(_cameraSolarSystemDefaultRotation);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }

    void ChangeCameraRotationInEditor(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation);
    }
    void ChangeCameraPositionInEditor(Vector3 position)
    {
        transform.position = _cameraSolarSystemDefaultPosition;
    }

    void enableOrDisableTrailRenderer(bool isLocked, GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            if (isLocked)
            {
                obj.GetComponent<TrailRenderer>().enabled = false;
            }

            if (!isLocked)
            {
                obj.GetComponent<TrailRenderer>().enabled = true;
            }
        }
    }
    //returns the basic values, if it's 0 than it's not active.
    private Vector3 GetBaseInput()
    { 
        Vector3 p_Velocity = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
