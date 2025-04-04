using UnityEngine;
using UnityEngine.Playables;

public class JumpState : State
{
    private bool _jumped;
    private float _timeSinceJump;
    private float _minJumpDuration = 0.2f; // Adjust as needed

    public JumpState(PlayerStateManager stateManager) : base(stateManager) { }

    public override void EnterState()
    {
        _jumped = false;
        _timeSinceJump = 0f;

        // TODO: Play jump animation here
        _stateManager.Animator.SetTrigger(AnimationParams.JumpTrigger);
    }

    public override void HandleInput(MyPlayerInputs inputs)
    {
        _stateManager.MoveInputVector = Vector3.ClampMagnitude(
            new Vector3(inputs.MoveAxisRight, 0f, inputs.MoveAxisForward),
            1f
        );

        Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.forward, _stateManager.Motor.CharacterUp).normalized;
        if (cameraPlanarDirection.sqrMagnitude == 0f)
        {
            cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.up, _stateManager.Motor.CharacterUp).normalized;
        }
        Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, _stateManager.Motor.CharacterUp);

        _stateManager.MoveInputVector = cameraPlanarRotation * _stateManager.MoveInputVector;
        _stateManager.LookInputVector = _stateManager.MoveInputVector.normalized;

        
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        _timeSinceJump += deltaTime;

        // Get right and forward directions in air using CharacterUp
        Vector3 inputRight = Vector3.Cross(_stateManager.MoveInputVector, _stateManager.Motor.CharacterUp);
        Vector3 reorientedInput = Vector3.Cross(_stateManager.Motor.CharacterUp, inputRight).normalized * _stateManager.MoveInputVector.magnitude;

        // Target movement velocity in air
        Vector3 targetMovementVelocity = reorientedInput * _stateManager.AirMoveSpeed;

        // Smoothly interpolate to target movement velocity (air control)
        currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1f - Mathf.Exp(-_stateManager.AirControlSharpness * deltaTime));

        // Apply gravity
        currentVelocity.y += _stateManager.Gravity.y * deltaTime;

        if (!_jumped && _stateManager.Motor.GroundingStatus.IsStableOnGround)
        {
            currentVelocity += (_stateManager.Motor.CharacterUp * _stateManager.JumpSpeed) - Vector3.Project(currentVelocity, _stateManager.Motor.CharacterUp);
            _jumped = true;
            _stateManager.Motor.ForceUnground();
        }
        else
        {
            currentVelocity += _stateManager.Gravity * deltaTime;
        }

        if (_jumped && _timeSinceJump >= _minJumpDuration && _stateManager.Motor.GroundingStatus.IsStableOnGround)
        {
            _stateManager.SwitchState(_stateManager.MoveState);
            _stateManager.Animator.ResetTrigger(AnimationParams.JumpTrigger);
        }
    }
}
