using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private Transform _cameraFollowPoint;
    [SerializeField] private PlayerStateManager _stateManager;

    private Vector3 _lookInputVector;

    private void Start()
    {
        _playerCamera.SetFollowTransform(_cameraFollowPoint);
    }

    private void HandleCameraInput()
    {
        float mouseUp = Input.GetAxisRaw("Mouse Y");
        float mouseRight = Input.GetAxisRaw("Mouse X");

        _lookInputVector = new Vector3(mouseRight, mouseUp, 0f);

        float scrollInput = -Input.GetAxis("Mouse ScrollWheel");
        _playerCamera.UpdateWithInput(Time.deltaTime, scrollInput, _lookInputVector);
    }

    private void HandleCharacterInputs()
    {
        MyPlayerInputs inputs = new MyPlayerInputs
        {
            MoveAxisForward = Input.GetAxisRaw("Vertical"),
            MoveAxisRight = Input.GetAxisRaw("Horizontal"),
            CameraRotation = _playerCamera.transform.rotation,
            JumpPressed = Input.GetKeyDown(KeyCode.Space),
            ChopPressed = Input.GetMouseButtonDown(0)
        };

        _stateManager.SetInputs(inputs);
    }

    private void Update()
    {
        HandleCharacterInputs();
    }

    private void LateUpdate()
    {
        HandleCameraInput();
    }
}
