using System.Collections.Generic;
using UnityEngine;

public class FollowState : CharacterStateBase
{
    #region Editor Fields

    [SerializeField] private float _moveSpeed;

    #endregion

    public override void Start()
    {
        base.Start();
    }

    public override void EnterState(Dictionary<string, object> data = null)
    {
        _stateMachine.Agent.enabled = true;
        _stateMachine.Agent.updateRotation = false;
        _stateMachine.Agent.speed = _moveSpeed;
    }

    public override void OnUpdate()
    {
        CheckExitState();

        _stateMachine.Agent.SetDestination(CharacterManager.Instance.CurrentCharacter.transform.position);

        _stateMachine.Anim.SetFloat("MoveX", _stateMachine.Agent.velocity.x);
        _stateMachine.Anim.SetFloat("MoveY", _stateMachine.Agent.velocity.z);

        //Remove when I get directional anims
        _stateMachine.SpriteRenderer.flipX = _stateMachine.Agent.velocity.z < 0f ? true : false;
    }

    public override void CheckExitState()
    {
        //TODO and add if not in combat
        if(_stateMachine.ControlId == CharacterManager.Instance.CurrentCharacter.ControlId) { _stateMachine.DoRoam(); }
    }
    public override void ExitState()
    {

    }
}
