using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerCamera _playerCamera;
    [SerializeField]
    private Transform _cameraFollowPoint;
    [SerializeField]
    private CharacterController _characterController;

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
        PlayerInputs inputs = new PlayerInputs();
        inputs.MoveAxisForward = Input.GetAxisRaw("Vertical");
        inputs.MoveAxisRight = Input.GetAxisRaw("Horizontal");
        inputs.CameraRotation = _playerCamera.transform.rotation;
        inputs.JumpPressed = Input.GetKeyDown(KeyCode.Space);

        _characterController.SetInputs(ref inputs);
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
