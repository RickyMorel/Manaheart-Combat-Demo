 using System.Collections.Generic;
using UnityEngine;

public class CombatState : CharacterStateBase
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
        _stateMachine.Agent.enabled = false;
    }

    public override void OnUpdate()
    {
        CheckExitState();
    }

    public override void CheckExitState()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _stateMachine.DoSlashAttack();  
        }
    }
    public override void ExitState()
    {

    }
}
