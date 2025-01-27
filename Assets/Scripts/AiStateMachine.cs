using UnityEngine;

public class AiStateMachine : CharacterStateMachine
{
    #region Editor Fields

    [SerializeField] private SpriteRenderer _selectedArrow;

    #endregion

    #region Public Properties

    public SpriteRenderer SelectedArrrow => _selectedArrow;

    #endregion

    public void SelectAsTarget(bool isSelected)
    {
        _selectedArrow.gameObject.SetActive(isSelected);   
    }
}
