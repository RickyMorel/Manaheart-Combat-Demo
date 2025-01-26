
using UnityEngine;
using UnityEngine.AI;

public class CharacterStateMachine : MonoBehaviour
{
    #region Editor Fields

    [Header("Components")]
    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Properties")]
    [SerializeField] private int _controlId;
    [SerializeField] private CharacterStateBase[] _states;

    #endregion

    #region Private Varaibles

    private CharacterStateBase _currentState;
    private NavMeshAgent _agent;

    #endregion

    #region Public Properties

    public int ControlId => _controlId;
    public NavMeshAgent Agent => _agent;
    public Animator Anim => _anim;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    #endregion

    protected void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        DoRoam();
    }

    protected void Update()
    {
        if(_currentState == null) { return; }

        _currentState.OnUpdate();   
    }

    public void DoState(CharacterStateBase state)
    {
        if(_currentState != null) {  _currentState.OnExitState(); }

        _currentState = state;

        _currentState.EnterState();
    }

    public void DoRoam() { DoState(_states[0]);  }
    public void DoFollow() { DoState(_states[1]);  }
    public void DoEnterCombatState() { DoState(_states[2]); }
    public void DoSlashAttack() { DoState(_states[3]); }

    public void SetTurn()
    {
        
    }
}
