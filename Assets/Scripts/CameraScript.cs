using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    #region Private Varaibles

    private CinemachineCamera _vcam;

    #endregion

    private void Start()
    {
        _vcam = GetComponent<CinemachineCamera>();

        CharacterManager.Instance.OnSwitchCharacter += HandleSwitchCharacter;
    }

    private void OnDestroy()
    {
        CharacterManager.Instance.OnSwitchCharacter -= HandleSwitchCharacter;
    }

    private void HandleSwitchCharacter(PlayableCharacter character)
    {
        _vcam.Follow = character.transform;   
    }
}
