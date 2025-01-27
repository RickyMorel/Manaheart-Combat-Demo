using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLocation : MonoBehaviour
{
    #region Editor Fields

    [SerializeField] private Transform[] _playerLocations;
    [SerializeField] private Transform[] _enemyLocations;
    [SerializeField] private AiStateMachine[] _enemies;

    #endregion

    #region Public Properties

    public static event Action<AiStateMachine[]> OnEnterCombat;

    #endregion

    #region Private Variables

    private float _moveDuration = 1f;

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        //Only start combat if player enters area
        if(!other.gameObject.TryGetComponent<PlayableCharacter>(out PlayableCharacter player)) { return; }

        if(player != CharacterManager.Instance.CurrentCharacter) { return; }

        StartCoroutine(MoveCharactersToPosition());
    }

    private IEnumerator MoveCharactersToPosition()
    {
        // Lists to store movement data for enemies and players
        List<MovementData> enemyMovements = new List<MovementData>();
        List<MovementData> playerMovements = new List<MovementData>();

        InitializeMovementData(enemyMovements, playerMovements);

        yield return LerpToPositions(enemyMovements, playerMovements);

        SnapToFinalPositions(enemyMovements, playerMovements);

        OnEnterCombat?.Invoke(_enemies);
    }

    private IEnumerator LerpToPositions(List<MovementData> enemyMovements, List<MovementData> playerMovements)
    {
        float elapsedTime = 0f;

        // Lerp all characters to their positions
        while (elapsedTime < _moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _moveDuration);

            // Move enemies
            foreach (MovementData movement in enemyMovements)
            {
                movement.Transform.position = Vector3.Lerp(movement.StartPosition, movement.TargetPosition, t);
            }

            // Move players
            foreach (MovementData movement in playerMovements)
            {
                movement.Transform.position = Vector3.Lerp(movement.StartPosition, movement.TargetPosition, t);
            }

            yield return null;
        }
    }

    private void InitializeMovementData(List<MovementData> enemyMovements, List<MovementData> playerMovements)
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            Transform enemyTransform = _enemies[i].transform;
            Vector3 targetPos = _enemyLocations[i].position;
            enemyMovements.Add(new MovementData(enemyTransform, targetPos));
        }

        for (int i = 0; i < CharacterManager.Instance.AllCharacters.Length; i++)
        {
            Transform playerTransform = CharacterManager.Instance.AllCharacters[i].transform;
            Vector3 targetPos = _playerLocations[i].position;
            playerMovements.Add(new MovementData(playerTransform, targetPos));
        }
    }

    private static void SnapToFinalPositions(List<MovementData> enemyMovements, List<MovementData> playerMovements)
    {
        // Ensure final positions are exact
        foreach (MovementData movement in enemyMovements)
        {
            movement.Transform.position = movement.TargetPosition;
        }

        foreach (MovementData movement in playerMovements)
        {
            movement.Transform.position = movement.TargetPosition;
        }
    }

    #region Helper Classes

    private class MovementData
    {
        public Transform Transform;
        public Vector3 StartPosition;
        public Vector3 TargetPosition;

        public MovementData(Transform transform, Vector3 target)
        {
            Transform = transform;
            StartPosition = transform.position;
            TargetPosition = target;
        }
    }

    #endregion
}
