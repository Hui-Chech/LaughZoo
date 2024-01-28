using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    #region Singleton
    public static PlayerControl Instance;

    private void SingletonInit()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private SpriteManager spriteManager;

    [Header("Juicy Settings")]
    [SerializeField] private float sizeOnStamp;
    [SerializeField] private float stampDuration;
    [SerializeField] private LeanTweenType type;

    public bool isGameStarted = false;
    private Camera mainCam;
    private GameObject spriteObject;
    private Vector3 originalSpriteSize;
    private Collider2D _collider;

    public bool isFaceDone { get; private set; } = false;
    private bool[] partsCheck;
    private int finshIndex = 0;


    [SerializeField] private List<GameObject> partsObject = new List<GameObject>();


    public int GetFinshIndex { get { return finshIndex; } set { finshIndex = value; } }
    private void Awake()
    {
        SingletonInit();

        Init();
    }

    private void Init()
    {
        mainCam = Camera.main;
        spriteObject = transform.GetChild(0).gameObject;
        _collider = GetComponent<Collider2D>();
        partsCheck = new bool[System.Enum.GetNames(typeof(PartType)).Length];

        originalSpriteSize = spriteObject.transform.localScale;
        _collider.enabled = false;
    }

    private void NewFaceInit()
    {
        // Clear parts
        if (partsObject.Count > 0)
        {
            for (int i = 0; i < partsObject.Count; i++)
            {
                Destroy(partsObject[i]);
            }
            partsObject.Clear();
        }

        // Random face base
        spriteObject.GetComponent<SpriteRenderer>().sprite = spriteManager.faceParts.spriteBase[Random.Range(0, spriteManager.faceParts.spriteBase.Count)];

        // Clear check
        for (int i = 0; i < partsCheck.Length; i++)
        {
            partsCheck[i] = false;
        }
    }

    private void Start()
    {
        NewFaceInit();
    }

    void Update()
    {
        if (!isGameStarted) return;

        FaceMovement();

        if (Input.GetMouseButtonDown(0))
        {
            StampOnFace();
        }
    }

    // Player face movement control
    private void FaceMovement()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCam.transform.position.z);
        transform.position = mainCam.ScreenToWorldPoint(mousePosition);
    }

    // Player stamp control
    private void StampOnFace()
    {
        LeanTween.cancel(gameObject);
        LeanTween.cancel(spriteObject);

        _collider.enabled = true;
        LeanTween.delayedCall(gameObject, stampDuration, () => _collider.enabled = false);

        // Juicy effect
        spriteObject.transform.localScale = originalSpriteSize;
        LeanTween.scale(spriteObject, originalSpriteSize * sizeOnStamp, stampDuration).setEase(type).setLoopPingPong(1);
    }

    private void FaceDoneCheck()
    {
        foreach (bool part in partsCheck)
        {
            if (!part) return;
        }

        isFaceDone = true;
        finshIndex++;
        AudioManager.PlayAudio(spriteManager.faceParts.NorthDudeAudio[Random.Range(0, spriteManager.faceParts.NorthDudeAudio.Count)], 1);
        Debug.Log("One face done!");

        NewFaceInit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var part = collision.GetComponent<FacePartBase>();
        if (part == null) return;

        partsObject.Add(part.gameObject);
        var type = part.PartSmashed(spriteObject.transform);

        if (partsCheck[(int)type]) return;

        partsCheck[(int)type] = true;
        FaceDoneCheck();
    }
}
