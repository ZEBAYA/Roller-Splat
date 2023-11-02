using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerCountdown;
    public static GameManager singleton;

    private GroundPiece[] allGroundPieces;
    private float timer;
    public bool isGameActive;

    private void Start()
    {
        SetupNewLevel();
        timer = 25;
        isGameActive=true;
    }
   

    private void SetupNewLevel()
    {
        allGroundPieces = FindObjectsOfType<GroundPiece>();
    }

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetupNewLevel();
        timer = 25;
        isGameActive = true;
    }

    public void CheckComplete()
    {
        bool isFinished = true;

        for (int i = 0; i < allGroundPieces.Length; i++)
        {
            if (allGroundPieces[i].isColored == false)
            {
                isFinished = false;
                break;
            }
        }

        if (isFinished)
            NextLevel();
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Update();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Update()
    {
        if(isGameActive)
        {
            timer -= Time.deltaTime;
            timerCountdown.SetText("Timer: " + Mathf.Round(timer));
            if (timer < 0)
            {
                RestartGame();
            }
        }
    }
}