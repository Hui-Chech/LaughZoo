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

    [Header("Juicy Settings")]
    [SerializeField] private float sizeOnStamp;
    [SerializeField] private float stampDuration;
    [SerializeField] private LeanTweenType type;

    public bool isGameStarted = false;
    private Camera mainCam;
    private GameObject spriteObject;
    private Vector3 originalSpriteSize;
    private Collider2D _collider;

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

        originalSpriteSize = spriteObject.transform.localScale;
        _collider.enabled = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var part = collision.GetComponent<FacePartBase>();
        if (part == null) return;

        part.PartSmashed(spriteObject.transform);
    }
}
