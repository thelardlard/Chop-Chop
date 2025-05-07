using UnityEngine;

public class ChopState : State
{
    private RhythmMinigame _rhythmMinigame;
    private Tree _targetTree;
    private int _successfulHits;

    public ChopState(PlayerStateManager stateManager) : base(stateManager) { }

    public override void EnterState()
    {
        _stateManager.Animator.SetTrigger(AnimationParams.ChopTrigger);

        // Get the targeted tree
        _targetTree = _stateManager.Interactor.GetTargetTree();

        if (_targetTree != null)
        {
            _rhythmMinigame = UIManager.Instance.StartRhythmMinigame(OnRhythmComplete, _targetTree);
        }
        else
        {
            Debug.LogError("No tree targeted for chopping!");
            _stateManager.SwitchState(_stateManager.MoveState);
        }
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        currentVelocity = Vector3.zero;
        // No movement during chopping
    }

    private void OnRhythmComplete(int successfulHits)
    {
        _successfulHits = successfulHits;
        int logsToSpawn = Mathf.Max(1, _successfulHits / 3); // Minimum 1 log

        _targetTree.FallTree(logsToSpawn); // <- Always fall, pass logs to spawn

        _stateManager.SwitchState(_stateManager.MoveState);
    }

    public override void ExitState()
    {
        _stateManager.Animator.ResetTrigger(AnimationParams.ChopTrigger);

        if (_rhythmMinigame != null)
        {
            _rhythmMinigame.CloseMinigame();
            _rhythmMinigame = null;
        }
    }
}
