using UnityEngine;

public abstract class State
{
    protected PlayerStateManager _stateManager;

    public State(PlayerStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void HandleInput(MyPlayerInputs inputs) { }
    public virtual void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime) { }
    public virtual void UpdateRotation(ref Quaternion currentRotation, float deltaTime) { }
}
