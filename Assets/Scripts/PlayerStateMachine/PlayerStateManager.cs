using UnityEngine;
using KinematicCharacterController;
using UnityEngine.Playables;

public class PlayerStateManager : MonoBehaviour, ICharacterController
{
    public KinematicCharacterMotor Motor;
    public PlayerInteractor Interactor;
    public Animator Animator; // Assign this in the inspector or via script

    public Vector3 MoveInputVector;
    public Vector3 LookInputVector;
    public Vector3 Gravity = new Vector3(0, -30f, 0);

    public float MaxStableMoveSpeed = 10f;
    public float StableMovementSharpness = 15f;
    public float OrientationSharpness = 10f;
    public float JumpSpeed = 10f;
    public float AirMoveSpeed = 10f;
    public float AirControlSharpness = 3f;

    private State _currentState;
    public MoveState MoveState;
    public JumpState JumpState;
    public ChopState ChopState;
    public string CurrentStateName => _currentState?.GetType().Name ?? "None";

    private void Awake()
    {
        MoveState = new MoveState(this);
        JumpState = new JumpState(this);
        ChopState = new ChopState(this);
    }

    private void Start()
    {
        Motor.CharacterController = this;
        SwitchState(MoveState);
        //Debug.Log("Animator name: " + Animator?.gameObject.name);
    }

    private void Update()
    {
        if (Animator == null)
        {
            Debug.LogWarning("Animator is NULL on PlayerStateManager!");
        }
    }

    public void SwitchState(State newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }

    public void SetInputs(MyPlayerInputs inputs)
    {
        _currentState.HandleInput(inputs);
    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        _currentState.UpdateVelocity(ref currentVelocity, deltaTime);
    }

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        _currentState.UpdateRotation(ref currentRotation, deltaTime);
    }

    public void BeforeCharacterUpdate(float deltaTime) { }
    public void AfterCharacterUpdate(float deltaTime) { }
    public void PostGroundingUpdate(float deltaTime) { }
    public bool IsColliderValidForCollisions(Collider coll) => true;
    public void OnDiscreteCollisionDetected(Collider hitCollider) { }

    void ICharacterController.OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        
    }

    void ICharacterController.OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        
    }

    void ICharacterController.ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
        
    }
}
