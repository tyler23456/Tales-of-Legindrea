using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using FirstGearGames.SmoothCameraShaker;

public interface IFactory
{
    ICrosshair crosshair { get; }
    public Dictionary<string, GameObject> props { get; }
    Dictionary<string, IEquipment> equipment { get; }
    Dictionary<string, List<AudioClip>> hitSFX { get; }
    Dictionary<string, List<AudioClip>> StepSFX { get; }
    Dictionary<string, AudioClip> generalSFX { get; }
    Dictionary<string, Sprite> itemIcons { get; }
    Dictionary<string, GameObject> uIElements { get; }
    Dictionary<string, Sprite> sprites { get; }
    Dictionary<string, ShakeData> shakeDatas { get; }
    Dictionary<string, IMission> missions { get; }

    GameObject getPlayer { get; }
    GameObject getMainCamera { get; }
    CinemachineVirtualCamera getCamVirtual { get; }
    Cinemachine3rdPersonFollow getFollow { get; }
    AudioSource getAudioSource { get; }
    GameObject getDamageText { get; }

    void MovePlayerTo(Vector3 position, Vector3 forward);
    RaycastHit RaycastFromCamera(float distance);
}
