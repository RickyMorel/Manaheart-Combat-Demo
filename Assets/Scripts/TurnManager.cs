using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    #region Private Variables

    private List<CharacterStateMachine> _allAgents = new List<CharacterStateMachine>();
    private int _agentTurnIndex;

    #endregion

    #region Public Properties

    public static TurnManager Instance;

    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CombatLocation.OnEnterCombat += OnStartCombat;
    }

    private void OnDestroy()
    {
        CombatLocation.OnEnterCombat -= OnStartCombat;
    }

    public void OnStartCombat(AiStateMachine[] enemies)
    {
        _allAgents = new List<CharacterStateMachine>();
        _allAgents.AddRange(CharacterManager.Instance.AllCharacters);
        _allAgents.AddRange(enemies);

        foreach (CharacterStateMachine agent in _allAgents)
        {
            agent.DoEnterCombatState();
        }
    }

    public void EndCombat()
    {
        foreach (CharacterStateMachine agent in _allAgents)
        {
            agent.DoRoam();
        }
    }
}
