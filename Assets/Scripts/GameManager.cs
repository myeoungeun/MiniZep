using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 점수 관리 (FlappyPlane 전용)
    private int currentScore = 0;
    public int CurrentScore => currentScore;

    // UIManager 참조 (FlappyPlane 씬용)
    private UIManager uiManager;
    

    // 현재 게임 상태
    public enum GameState { MainScene, FlappyMiniGame }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 변경해도 유지
            SceneManager.sceneLoaded += OnSceneLoaded;
            CurrentState = GameState.MainScene;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 씬 변경 시마다 UIManager 찾아서 연결하기 (FlappyPlane 씬에서만)
        if (CurrentState == GameState.FlappyMiniGame)
        {
            uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                uiManager.UpdateScore(currentScore);
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        if (sceneName == "MainScene")
            CurrentState = GameState.MainScene;
        else if (sceneName == "FlappyPlane")
            CurrentState = GameState.FlappyMiniGame;

        currentScore = 0; // 씬 전환 시 점수 초기화(필요하면 조정)
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드된 직후에 호출되는 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FlappyPlane")
        {
            CurrentState = GameState.FlappyMiniGame;
            
            currentScore = 0;

            uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                uiManager.UpdateScore(currentScore);
            }
        }
        else if (scene.name == "MainScene")
        {
            CurrentState = GameState.MainScene;
        }
    }

    // FlappyPlane 씬 전용 점수 증가 함수
    public void AddScore(int score)
    {
        if (CurrentState != GameState.FlappyMiniGame) return;

        currentScore += score;
        Debug.Log("Score: " + currentScore);

        if (uiManager != null)
        {
            uiManager.UpdateScore(currentScore);
        }
    }

    // 게임오버 처리 (FlappyPlane)
    public void GameOver()
    {
        Debug.Log("Game Over");
        if (uiManager != null)
        {
            uiManager.SetRestart();
        }
    }

    // 게임 재시작 (FlappyPlane)
    public void RestartGame()
    {
        if (CurrentState == GameState.FlappyMiniGame)
        {
            SceneManager.LoadScene("FlappyPlane");
        }
    }
}
