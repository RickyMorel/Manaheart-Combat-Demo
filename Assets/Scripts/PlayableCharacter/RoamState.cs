using System.Collections.Generic;
using UnityEngine;

public class RoamState : CharacterStateBase
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

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical") * -1f;

        Vector3 moveDir = new Vector3(verticalInput, 0f, horizontalInput);

        moveDir.Normalize();

        Vector3 movement = moveDir * _moveSpeed * Time.deltaTime;

        transform.Translate(movement, Space.World);

        _stateMachine.Anim.SetFloat("MoveX", horizontalInput);
        _stateMachine.Anim.SetFloat("MoveY", verticalInput);

        //Remove when I get directional anims
        _stateMachine.SpriteRenderer.flipX = horizontalInput < 0 ? true : false;
    }

    public override void CheckExitState()
    {
        //TODO and add if not in combat
        if(_stateMachine.ControlId != CharacterManager.Instance.CurrentCharacter.ControlId) { _stateMachine.DoFollow(); }
    }
    public override void OnExitState()
    {
        _stateMachine.Anim.SetFloat("MoveX", 0f);
        _stateMachine.Anim.SetFloat("MoveY", 0f);
    }
}
