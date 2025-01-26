using UnityEngine;

public abstract class CharacterStateBase : MonoBehaviour
{
    #region Private Variables

    protected CharacterStateMachine _stateMachine;

    #endregion

    public virtual void Start()
    {
        _stateMachine = GetComponent<CharacterStateMachine>();
    }

    public virtual void EnterState()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void CheckExitState()
    {

    }
    public virtual void ExitState()
    {

    }
}
