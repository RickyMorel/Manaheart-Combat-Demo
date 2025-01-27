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


    public void StartCombat()
    {
        _allAgents = FindObjectsOfType<CharacterStateMachine>().ToList();

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
