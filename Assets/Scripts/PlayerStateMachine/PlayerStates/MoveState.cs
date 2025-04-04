using UnityEngine;
using UnityEngine.Playables;

public class MoveState : State
{
    public MoveState(PlayerStateManager stateManager) : base(stateManager) { }

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

        if (inputs.JumpPressed)
        {
            _stateManager.SwitchState(_stateManager.JumpState);
        }
        else if (inputs.ChopPressed && _stateManager.Interactor.HasTargetTree())
        {
            _stateManager.SwitchState(_stateManager.ChopState);
        }

        // TODO: Play move animation here
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        if (_stateManager.Motor.GroundingStatus.IsStableOnGround)
        {
            float currentVelocityMagnitude = currentVelocity.magnitude;
            Vector3 effectiveGroundNormal = _stateManager.Motor.GroundingStatus.GroundNormal;
            currentVelocity = _stateManager.Motor.GetDirectionTangentToSurface(currentVelocity, effectiveGroundNormal) * currentVelocityMagnitude;

            Vector3 inputRight = Vector3.Cross(_stateManager.MoveInputVector, _stateManager.Motor.CharacterUp);
            Vector3 reorientedInput = Vector3.Cross(effectiveGroundNormal, inputRight).normalized * _stateManager.MoveInputVector.magnitude;

            Vector3 targetMovementVelocity = reorientedInput * _stateManager.MaxStableMoveSpeed;
            currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1f - Mathf.Exp(-_stateManager.StableMovementSharpness * deltaTime));
        }
        else
        {
            currentVelocity += _stateManager.Gravity * deltaTime;
        }

        _stateManager.Animator.SetFloat(AnimationParams.Speed, _stateManager.MoveInputVector.magnitude);
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        if (_stateManager.LookInputVector.sqrMagnitude > 0f && _stateManager.OrientationSharpness > 0f)
        {
            Vector3 smoothedLookInputDirection = Vector3.Slerp(
                _stateManager.Motor.CharacterForward,
                _stateManager.LookInputVector,
                1 - Mathf.Exp(-_stateManager.OrientationSharpness * deltaTime)
            ).normalized;

            currentRotation = Quaternion.LookRotation(smoothedLookInputDirection, _stateManager.Motor.CharacterUp);
        }
    }
}
