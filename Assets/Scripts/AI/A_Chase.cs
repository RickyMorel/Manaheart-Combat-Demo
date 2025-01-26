using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class A_Chase : A_Base
{
    #region Editor Fields

    [SerializeField] private float _attackDistance = 3f;

    #endregion

    #region Private Variables

    private NavMeshAgent _agent;
    private bool _isAttacking;

    #endregion

    public override void Start()
    {
        base.Start();

        _agent = GetComponent<NavMeshAgent>();
    }

    public override void StartAction(Dictionary<string, object> data = null)
    {
        base.StartAction(data);
    }

    public override void DoAction()
    {
        if(_isAttacking) { return; }

        CheckSwitchAction();

        ChasePlayer();
    }

    private void ChasePlayer()
    {

    }

    private IEnumerator AttackPlayer()
    {
        _isAttacking = true;

        Debug.Log("ATTACK!");

        _agent.isStopped = true;

        _aiStateMachine.Anim.Play("PunchAttack", 0);

        yield return new WaitForSeconds(4f);

        _isAttacking = false;

        _agent.isStopped = false;
    }

    public override void CheckSwitchAction()
    {
        base.CheckSwitchAction();
    }
}
