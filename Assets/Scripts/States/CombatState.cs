 using System.Collections.Generic;
using UnityEngine;

public class CombatState : CharacterStateBase
{
    #region Editor Fields

    #endregion

    public override void Start()
    {
        base.Start();
    }

    public override void EnterState(Dictionary<string, object> data = null)
    {
        _stateMachine.Agent.enabled = false;
        _stateMachine.Anim.SetBool("IsInCombat", true);
    }

    public override void OnUpdate()
    {
        CheckExitState();
    }

    public override void CheckExitState()
    {
        if(_stateMachine != CharacterManager.Instance.CurrentCharacter) { return; }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _stateMachine.DoSlashAttack();  
        }
    }

    public override void OnExitState()
    {
        _stateMachine.Anim.SetBool("IsInCombat", false);
    }
}
