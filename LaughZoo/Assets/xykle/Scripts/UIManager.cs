using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void SingletonInit()
    {
        Instance = this;
    }

    [SerializeField] private RectTransform scoreCanvas;
    [SerializeField] private Image shotImage;
    [SerializeField] private CanvasGroup shotGroup;
    [SerializeField] private List<Image> scoreImage;
    [SerializeField] private SpriteManager spriteManager;

    [Header("Juicy Settings")]
    [SerializeField] private float shotDisplayTime = .5f;
    [SerializeField] private float fadeTime;
    [SerializeField] private float fadeSize;
    [SerializeField] private LeanTweenType fadeType;
    [SerializeField] private float scoreScaleTime;
    [SerializeField] private float scoreScaleSize;
    [SerializeField] private LeanTweenType scoreType;

    private Vector3 originalSize;

    private void Awake()
    {
        SingletonInit();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        originalSize = shotImage.transform.localScale;
        shotGroup.alpha = 0f;
    }

    public void FaceShot(Sprite sprite)
    {
        shotGroup.alpha = 0f;
        shotImage.sprite = sprite;

        LeanTween.alphaCanvas(shotGroup, 1f, .25f);
        LeanTween.scale((RectTransform)shotImage.transform, originalSize, .25f).setEaseOutBounce();

        LeanTween.delayedCall(shotDisplayTime, () =>
        {
            LeanTween.alphaCanvas(shotGroup, 0f, fadeTime).setEase(fadeType);
            LeanTween.scale((RectTransform)shotImage.transform, Vector3.one * fadeSize, fadeTime).setEase(fadeType);
        });
    }

    public void UpdateScore(int score)
    {
        scoreImage[0].sprite = spriteManager.faceParts.NumberSprites[score / 10];
        scoreImage[1].sprite = spriteManager.faceParts.NumberSprites[score % 10];

        for(int i = 0; i < 2; i++)
        {
            LeanTween.cancel((RectTransform)scoreImage[i].transform);

            scoreImage[i].transform.localScale = Vector3.one;
            LeanTween.scale((RectTransform)scoreImage[i].transform, Vector3.one * scoreScaleSize, scoreScaleTime).setEase(scoreType).setLoopPingPong(1);
        }
    }
}
