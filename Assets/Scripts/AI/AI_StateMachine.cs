using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_StateMachine : MonoBehaviour
{
    #region Editor Fields

    [SerializeField] private CharacterStateBase[] _actions;

    #endregion

    #region Private Variables

    private CharacterStateBase _currentAction;
    private CharacterStateBase _prevAction;
    private AI_Cues _aiCues;
    private Animator _anim;
    private NavMeshAgent _agent;

    #endregion

    #region Public Properties

    public Animator Anim => _anim;

    #endregion

    void Start()
    {
        _aiCues = GetComponent<AI_Cues>();
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        DoNewAction(_actions[0]);   
    }

    void Update()
    {
        Animate();

        if(!_currentAction) { return; }

        _currentAction.OnUpdate();
    }

    private void Animate()
    {
        _anim.SetFloat("Moving", _agent.velocity.magnitude / _agent.speed);
    }

    private void DoNewAction(CharacterStateBase actionToDo, Dictionary<string, object> data = null)
    {
        _prevAction = _currentAction;

        actionToDo.EnterState(data);

        _currentAction = actionToDo;
    }

    public void DoPrevAction()
    {
        _prevAction.EnterState();

        _currentAction = _prevAction;
    }

    public void DoChase() { DoNewAction(_actions[1]); }
}
