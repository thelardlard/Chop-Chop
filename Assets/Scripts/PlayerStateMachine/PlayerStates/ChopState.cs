using UnityEngine;
using UnityEngine.Playables;

public class ChopState : State
{
    private float _chopTimer;
    private float _chopDuration = 0.78f;

    public ChopState(PlayerStateManager stateManager) : base(stateManager) { }

    public override void EnterState()
    {
        _chopTimer = 0f;
        _stateManager.Animator.SetTrigger(AnimationParams.ChopTrigger);

        // TODO: Play chop start animation here
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        currentVelocity = Vector3.zero;

        _chopTimer += deltaTime;
        if (_chopTimer >= _chopDuration)
        {
            // TODO: Trigger chop hit animation or effect here
            _stateManager.Interactor.ChopTree();
            _stateManager.SwitchState(_stateManager.MoveState);
        }
    }

    public override void ExitState()
    {

        _stateManager.Animator.ResetTrigger(AnimationParams.ChopTrigger);
    }
}
