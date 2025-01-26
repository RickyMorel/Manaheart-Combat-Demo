using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStateBase : MonoBehaviour
{
    #region Private Variables

    protected CharacterStateMachine _stateMachine;

    #endregion

    private void Awake()
    {
        Debug.Log("GET STATE MAC]HINE");
        _stateMachine = GetComponent<CharacterStateMachine>();
    }

    public virtual void Start()
    {

    }

    public virtual void EnterState(Dictionary<string, object> data = null)
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void CheckExitState()
    {

    }
    public virtual void OnExitState()
    {

    }
}
