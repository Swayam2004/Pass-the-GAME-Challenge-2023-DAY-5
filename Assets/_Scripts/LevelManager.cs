using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public enum Scene
    {
        Level,
        LoadingScene,
        MainMenuScene,
    }

    [SerializeField] private RectTransform _transitionRectTransform;
    [SerializeField] private Material _transitionMaterial;

    private static Action _onLoaderCallBack;

    private Vector2 _startPos = new Vector3(-2600f, 0f, 0f);
    private Vector2 _endPos = new Vector3(2600f, 0f, 0f);

    public static bool _isStorySeen = false;

    private void Awake()
    {
        if (Instance != null)
        {
            //Debug.LogError("There is more than one LevelManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _transitionMaterial.SetFloat("_Circle_Radius", 1.5f);
    }

    private IEnumerator LoadSceneAsync(string targetScene)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetScene);

        if (!asyncOperation.isDone)
        {
            yield return null;
        }

        // This single line fixes the bug with Scene Loading and DOTween
        yield return new WaitForSeconds(0.5f);

        float transitionDuration = 0.5f;
        float circleRadius = 0f;
        _transitionMaterial.SetFloat("_Circle_Radius", circleRadius);
        DOTween.To(() => circleRadius, x =>
        {
            circleRadius = x;
            _transitionMaterial.SetFloat("_Circle_Radius", circleRadius);
        },
        1.5f, transitionDuration).SetEase(Ease.OutQuad).SetUpdate(true);
        //_transitionRectTransform.localPosition = Vector3.zero;
        //_transitionRectTransform.DOLocalMove(_endPos, transitionDuration).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    public void Load(string targetScene)
    {
        _onLoaderCallBack = () =>
        {
            StartCoroutine(LoadSceneAsync(targetScene));
        };

        float transitionDuration = 0.5f;
        float circleRadius = 1.4f;
        _transitionMaterial.SetFloat("_Circle_Radius", circleRadius);
        DOTween.To(() => circleRadius, x =>
        {
            circleRadius = x;
            _transitionMaterial.SetFloat("_Circle_Radius", circleRadius);
        },
        0f, transitionDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
        });
    }
    public void LoadWithoutAsync(string targetScene)
    {
        _onLoaderCallBack = () =>
        {
            SceneManager.LoadScene(targetScene);
        };

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    private IEnumerator LoadSceneAsync(int targetSceneIndex)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneIndex);

        if (!asyncOperation.isDone)
        {
            yield return null;
        }

        // This single line fixes the bug with Scene Loading and DOTween
        yield return new WaitForSeconds(0.5f);

        float transitionDuration = 0.5f;
        float circleRadius = 0f;
        _transitionMaterial.SetFloat("_Circle_Radius", circleRadius);
        DOTween.To(() => circleRadius, x =>
        {
            circleRadius = x;
            _transitionMaterial.SetFloat("_Circle_Radius", circleRadius);
        },
        1.5f, transitionDuration).SetEase(Ease.OutQuad).SetUpdate(true);
        //_transitionRectTransform.localPosition = Vector3.zero;
        //_transitionRectTransform.DOLocalMove(_endPos, transitionDuration).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    public void Load(int targetSceneIndex)
    {
        _onLoaderCallBack = () =>
        {
            StartCoroutine(LoadSceneAsync(targetSceneIndex));
        };

        float transitionDuration = 0.5f;
        float circleRadius = 1.4f;
        _transitionMaterial.SetFloat("_Circle_Radius", circleRadius);
        DOTween.To(() => circleRadius, x =>
        {
            circleRadius = x;
            _transitionMaterial.SetFloat("_Circle_Radius", circleRadius);
        },
        0f, transitionDuration).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
        });
    }

    public static void LoaderCallback()
    {
        if (_onLoaderCallBack != null)
        {
            _onLoaderCallBack();
            _onLoaderCallBack = null;
        }
    }
}
