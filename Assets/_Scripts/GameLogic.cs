using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance { get; private set; }
    public event EventHandler<WinTimerProgressEventArgs> OnWinTimerProgress;
    public class WinTimerProgressEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] private float winTimerMax = 3f;

    private float winTimer = 0f;
    private int levelId;
    private bool _canLoadScene = true;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GameLogic in scene!");
        }
        Instance = this;
        Scene currentScene = SceneManager.GetActiveScene();
        try
        {
            levelId = Int32.Parse(Regex.Replace(currentScene.name, "[^0-9]", ""));
        }
        catch
        {
            Debug.LogError("Could not parse level id from scene name: " + currentScene.name);
        }
    }

    void Update()
    {
        bool allSaved = true;
        foreach (Planet planet in FindObjectsOfType<Planet>())
        {
            if (!planet.IsSaved())
            {
                allSaved = false;
                break;
            }
        }
        if (allSaved)
        {
            SetWinTimer(winTimer + Time.deltaTime);
            if (winTimer >= winTimerMax)
            {
                LoadNextLevel();
            }
        }
        else
        {
            SetWinTimer(0f);
        }
    }

    private void SetWinTimer(float value)
    {
        winTimer = value;
        OnWinTimerProgress?.Invoke(this, new WinTimerProgressEventArgs { progressNormalized = value / winTimerMax });
    }

    private void LoadNextLevel()
    {
        string name = "Level " + (levelId + 1);
        bool exists = false;
        for (int i = 0; true; i++)
        {
            var s = SceneUtility.GetScenePathByBuildIndex(i);
            if (s.Length <= 0)
            {
                break;
            }
            else
            {
                if (s.Contains(name))
                {
                    exists = true;
                    break;
                }
            }
        }
        if (_canLoadScene)
        {
            if (exists)
            {
                LevelManager.Instance.Load(name);
            }
            else
            {
                LevelManager.Instance.Load("GameOver");
            }
            _canLoadScene = false;
        }
    }

    public void RetryLevel()
    {
        LevelManager.Instance.Load(SceneManager.GetActiveScene().name);
    }

    public void GoHome()
    {
        LevelManager.Instance.Load("MainMenu");
    }
}
