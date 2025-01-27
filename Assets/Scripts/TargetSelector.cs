using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    #region Private Variables

    private List<AiStateMachine> _allEnemies = new List<AiStateMachine>();
    private AiStateMachine _currentTarget;
    private int _currentTargetIndex = 0;

    #endregion

    #region Public Properties

    public static TargetSelector Instance { get; private set; }
    public AiStateMachine CurrentTarget => _currentTarget;

    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        CombatLocation.OnEnterCombat += HandleEnterCombat;
    }

    private void OnDestroy()
    {
        CombatLocation.OnEnterCombat -= HandleEnterCombat;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) { _currentTargetIndex--; }
        else if (Input.GetKeyDown(KeyCode.D)) {_currentTargetIndex++;}
        else { return; }

        if(_currentTargetIndex < 0) { _currentTargetIndex = _allEnemies.Count - 1; }
        if(_currentTargetIndex > (_allEnemies.Count - 1)) { _currentTargetIndex = 0; }

        SelectTarget();
    }

    private void HandleEnterCombat(AiStateMachine[] enemies)
    {
        _currentTargetIndex = 0;
        _currentTarget = null;

        Debug.Log("enemies: " + enemies.Length);

        _allEnemies = new List<AiStateMachine>(enemies);

        Debug.Log("_allEnemies: " + _allEnemies.Count);

        SelectTarget();
    }

    private void SelectTarget()
    {
        if(_allEnemies.Count < 1) { return; }

        Debug.Log("SelectTarget: " + _currentTargetIndex);

        foreach (AiStateMachine agent in _allEnemies)
        {
            agent.SelectAsTarget(false);
        }

        _currentTarget = _allEnemies[_currentTargetIndex];

       _currentTarget.SelectAsTarget(true);
    }
}
