using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Private Variables

    private PlayableCharacter _currentCharacter;
    private PlayableCharacter[] _allCharacters;
    private int _currentCharacterIndex = -1;

    #endregion

    #region Public Properties

    public static CharacterManager Instance { get; private set; }
    public PlayableCharacter CurrentCharacter => _currentCharacter;
    public PlayableCharacter[] AllCharacters => _allCharacters;

    public event Action<PlayableCharacter> OnSwitchCharacter;

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
        _allCharacters = FindObjectsOfType<PlayableCharacter>();

        CycleCharacter();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) {  CycleCharacter(); }
    }

    private void CycleCharacter()
    {
        _currentCharacterIndex++;

        if(_currentCharacterIndex >= _allCharacters.Length) { _currentCharacterIndex = 0; }

        _currentCharacter = _allCharacters[_currentCharacterIndex];

        OnSwitchCharacter?.Invoke(_currentCharacter);
    }
}
