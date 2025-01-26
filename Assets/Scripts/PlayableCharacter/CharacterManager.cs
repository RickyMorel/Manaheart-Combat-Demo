using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Private Variables

    private CharacterStateMachine _currentCharacter;
    private CharacterStateMachine[] _allCharacters;
    private int _currentCharacterIndex = -1;

    #endregion

    #region Public Properties

    public static CharacterManager Instance { get; private set; }
    public CharacterStateMachine CurrentCharacter => _currentCharacter;

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
        _allCharacters = FindObjectsOfType<CharacterStateMachine>();

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
    }
}
