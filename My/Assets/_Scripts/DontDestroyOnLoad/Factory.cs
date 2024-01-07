using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cinemachine;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine.Playables;

namespace GDA.DontDestroyOnLoad
{
    public class Factory : MonoBehaviour, IFactory
    {
        [SerializeField] GameObject player;
        [SerializeField] GameObject mainCamera;
        [SerializeField] CinemachineVirtualCamera camVirtual;
        [SerializeField] AudioSource audioSource;

        [SerializeField] AssetLabelReference propsReference;
        [SerializeField] AssetLabelReference promptsReference;
        [SerializeField] AssetLabelReference abilitiesReference;
        [SerializeField] AssetLabelReference armorReference;
        [SerializeField] AssetLabelReference playerHitReference;
        [SerializeField] AssetLabelReference generalSFXReference;
        [SerializeField] AssetLabelReference itemIconsReference;
        [SerializeField] AssetLabelReference uIElementsReference;
        [SerializeField] AssetLabelReference spritesReference;
        [SerializeField] AssetLabelReference shakeDataReference;
        [SerializeField] AssetLabelReference missionsReference;

        [SerializeField] AssetLabelReference genericHitReference;
        [SerializeField] AssetLabelReference woodHitReference;
        [SerializeField] AssetLabelReference metalHitReference;

        [SerializeField] AssetLabelReference concreteSprintReference;
        [SerializeField] AssetLabelReference concreteWalkReference;
        [SerializeField] AssetLabelReference dirtSprintReference;
        [SerializeField] AssetLabelReference dirtWalkReference;
        [SerializeField] AssetLabelReference emptySprintReference;
        [SerializeField] AssetLabelReference emptyWalkReference;
        [SerializeField] AssetLabelReference grassSprintReference;
        [SerializeField] AssetLabelReference grassWalkReference;
        [SerializeField] AssetLabelReference gravelSprintReference;
        [SerializeField] AssetLabelReference gravelWalkReference;
        [SerializeField] AssetLabelReference metalSprintReference;
        [SerializeField] AssetLabelReference metalWalkReference;
        [SerializeField] AssetLabelReference woodSprintReference;
        [SerializeField] AssetLabelReference woodWalkReference;

        Cinemachine3rdPersonFollow follow;

        public ICrosshair crosshair { get; private set; }
        public Dictionary<string, GameObject> props { get; private set; } = new Dictionary<string, GameObject>();
        public Dictionary<string, IEquipment> equipment { get; private set; } = new Dictionary<string, IEquipment>();
        public Dictionary<string, List<AudioClip>> hitSFX { get; private set; } = new Dictionary<string, List<AudioClip>>();
        public Dictionary<string, List<AudioClip>> StepSFX { get; private set; } = new Dictionary<string, List<AudioClip>>();
        public Dictionary<string, AudioClip> generalSFX { get; private set; } = new Dictionary<string, AudioClip>();
        public Dictionary<string, Sprite> itemIcons { get; private set; } = new Dictionary<string, Sprite>();
        public Dictionary<string, GameObject> uIElements { get; private set; } = new Dictionary<string, GameObject>();

        public Dictionary<string, Sprite> sprites { get; private set; } = new Dictionary<string, Sprite>();
        public Dictionary<string, ShakeData> shakeDatas { get; private set; } = new Dictionary<string, ShakeData>();
        public Dictionary<string, IMission> missions { get; private set; } = new Dictionary<string, IMission>();

        public GameObject getPlayer => player;
        public GameObject getMainCamera => mainCamera;
        public CinemachineVirtualCamera getCamVirtual => camVirtual;
        public Cinemachine3rdPersonFollow getFollow => follow;
        public AudioSource getAudioSource => audioSource;
        public GameObject getDamageText => uIElements["DamageText"];

        public void Awake()
        {
            audioSource.ignoreListenerPause = true;

            Addressables.LoadAssetsAsync<GameObject>(propsReference, (prop) =>
            {
                props.Add(prop.name, prop);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<IEquipment>(abilitiesReference, (ability) =>
            {
                equipment.Add(ability.getIcon.name, ability);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<Sprite>(itemIconsReference, (itemIcon) =>
            {
                itemIcons.Add(itemIcon.name, itemIcon);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<GameObject>(uIElementsReference, (uIElement) =>
            {
                uIElements.Add(uIElement.name, uIElement);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<Sprite>(spritesReference, (sprite) =>
            {
                sprites.Add(sprite.name, sprite);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<ShakeData>(shakeDataReference, (data) =>
            {
                shakeDatas.Add(data.name, data);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<IMission>(missionsReference, (data) =>
            {
                data.Initialize();
                missions.Add(data.getName, data);
            }).WaitForCompletion();

            List<AssetLabelReference> stepSFXLabelReferences = new List<AssetLabelReference>
            {
            concreteSprintReference, concreteWalkReference,
            dirtSprintReference, dirtWalkReference,
            emptySprintReference, emptyWalkReference,
            grassSprintReference, grassWalkReference,
            gravelSprintReference, gravelWalkReference,
            metalSprintReference, metalWalkReference,
            woodSprintReference, woodWalkReference
            };

            List<AudioClip> stepSFXType = null;
            foreach (AssetLabelReference label in stepSFXLabelReferences)
            {
                stepSFXType = new List<AudioClip>();
                Addressables.LoadAssetsAsync<AudioClip>(label, (clip) =>
                {
                    stepSFXType.Add(clip);
                }).WaitForCompletion();
                StepSFX.Add(label.labelString, stepSFXType);
            }


            List<AssetLabelReference> hitSFXLabelReferences = new List<AssetLabelReference>
            {
            playerHitReference,
            genericHitReference,
            woodHitReference,
            metalHitReference
            };

            List<AudioClip> hitSFXType = null;
            foreach (AssetLabelReference label in hitSFXLabelReferences)
            {
                hitSFXType = new List<AudioClip>();
                Addressables.LoadAssetsAsync<AudioClip>(label, (clip) =>
                {
                    hitSFXType.Add(clip);
                }).WaitForCompletion();
                hitSFX.Add(label.labelString, hitSFXType);
            }

            generalSFX = new Dictionary<string, AudioClip>();
            Addressables.LoadAssetsAsync<AudioClip>(generalSFXReference, (clip) =>
            {
                generalSFX.Add(clip.name, clip);
            }).WaitForCompletion();

            follow = camVirtual.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            crosshair = transform.Find("Canvas/Display1/Crosshair").GetComponent<ICrosshair>();
        }

        public void MovePlayerTo(Vector3 position, Vector3 forward)
        {
            Vector3 damping = follow.Damping;
            follow.Damping = Vector3.zero;
            player.SetActive(false);
            player.transform.position = position;
            player.transform.forward = forward;
            player.SetActive(true);
            camVirtual.UpdateCameraState(Vector3.up, -1);
            follow.Damping = damping;
        }

        public RaycastHit RaycastFromCamera(float distance)
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(screenCenterPoint);
            Physics.Raycast(ray, out RaycastHit hit, distance, LayerMask.NameToLayer("Player"));
            return hit;
        }
    }
}
