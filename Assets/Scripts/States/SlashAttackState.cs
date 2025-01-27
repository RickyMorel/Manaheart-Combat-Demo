using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttackState : CharacterStateBase
{
    #region Editor Fields

    [SerializeField] private float _attackDistance;

    #endregion

    #region Private Variables

    private Vector3 _initialAttackPos;

    #endregion

    public override void Start()
    {
        base.Start();
    }

    public override void EnterState(Dictionary<string, object> data = null)
    {
        StartCoroutine(AttackTarget());
    }

    private IEnumerator RunToTarget()
    {
        _initialAttackPos = transform.position; 

        _stateMachine.Agent.enabled = true;
        _stateMachine.Agent.stoppingDistance = _attackDistance;

        _stateMachine.Agent.SetDestination(TargetSelector.Instance.CurrentTarget.transform.position);

        _stateMachine.Anim.SetFloat("MoveY", 1f);

        bool isInAttackingDistance = false;

        while(!isInAttackingDistance) {
            isInAttackingDistance = Vector3.Distance(TargetSelector.Instance.CurrentTarget.transform.position, transform.position) <= _attackDistance;
            yield return null;
        }

        //Debug.Log("isInAttackingDistance: " + isInAttackingDistance);

        _stateMachine.Agent.enabled = false;

        _stateMachine.Anim.SetFloat("MoveY", 0f);
    }

    private IEnumerator RunBack()
    {
        _stateMachine.Agent.enabled = true;
        _stateMachine.Agent.stoppingDistance = 0f;

        _stateMachine.Agent.SetDestination(_initialAttackPos);

        _stateMachine.Anim.SetFloat("MoveY", 1f);

        bool IsBackInPlace = false;

        while (!IsBackInPlace)
        {
            IsBackInPlace = Vector3.Distance(_initialAttackPos, transform.position) < 0.5;
            yield return null;
        }

        _stateMachine.Agent.enabled = false;

        _stateMachine.Anim.SetFloat("MoveY", 0f);
    }

    private IEnumerator AttackTarget()
    {
        yield return RunToTarget();

        _stateMachine.Anim.Play("SlashAttack", 0);

        yield return new WaitForEndOfFrame();

        float attackAnimLength = _stateMachine.Anim.GetCurrentAnimatorClipInfo(0).Length;

        yield return new WaitForSeconds(attackAnimLength);

        yield return RunBack();

        _stateMachine.DoEnterCombatState();
    }
}
