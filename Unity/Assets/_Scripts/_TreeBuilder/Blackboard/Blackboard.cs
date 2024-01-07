using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

[System.Serializable]
public class Blackboard
{
    public static IMenu menu;
    public static IFactory factory;
    public static IHittable player;

    public static Dictionary<string, Transform> teleportDestinations = new Dictionary<string, Transform>();

    [Header("AI")]
    [HideInInspector] public NavMeshAgent agent;

    [Header("Animation")]
    [HideInInspector] public Animation animation;

    [Header("Progress")]
    [SerializeField] public int localProgress;

    [Header("Speed")]
    [SerializeField] public float speed;

    public IHittable user;
    public IHittable target;

    [HideInInspector] public GameObject gameObject;
    [HideInInspector] public Progress progress;
    [HideInInspector] public Item item;
    [HideInInspector] public HasItem hasItem;
    [HideInInspector] public Lock userLock;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public Renderer renderer;
    [HideInInspector] public IInteractableRunner interactableRunner;
    [HideInInspector] public Collider collider;
    [HideInInspector] public HasGameObject hasGameObject;
    [HideInInspector] public IActivator activator;
    [HideInInspector] public PlayableDirector playableDirector;
    [HideInInspector] public PlayableBinder playableBinder;
    [HideInInspector] public OtherAnimator otherAnimator;

    public Blackboard(GameObject gameObject)
    {
        this.gameObject = gameObject;

        if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).name == "SpawnPoint")
            teleportDestinations.Add(gameObject.name, gameObject.transform.GetChild(0));

        GameObject dontDestroyOnLoad = GameObject.Find("/DontDestroyOnLoad");
        menu = dontDestroyOnLoad.GetComponent<IMenu>();
        factory = dontDestroyOnLoad.GetComponent<IFactory>();
        player = factory.getPlayer.GetComponent<IHittable>();

        agent = gameObject.GetComponent<NavMeshAgent>();
        animation = gameObject.GetComponent<Animation>();

        user = gameObject.GetComponent<IHittable>();
        target = player;

        progress = gameObject.GetComponent<Progress>();
        animator = gameObject.GetComponent<Animator>();      
        audioSource = gameObject.GetComponent<AudioSource>();
        renderer = gameObject.GetComponent<Renderer>();
        interactableRunner = gameObject.GetComponent<IInteractableRunner>();
        collider = gameObject.GetComponent<Collider>();
        item = gameObject.GetComponent<Item>();
        hasGameObject = gameObject.GetComponent<HasGameObject>();
        activator = gameObject.GetComponent<IActivator>();
        playableDirector = gameObject.GetComponent<PlayableDirector>();
        playableBinder = gameObject.GetComponent<PlayableBinder>();
        otherAnimator = gameObject.GetComponent<OtherAnimator>();
    }
}

