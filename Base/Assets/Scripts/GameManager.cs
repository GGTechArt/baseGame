using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public enum GameState
{
    PREPARE,
    PLAYING,
    PAUSED,
    FINISHED
}
public class GameManager : ServiceInstallerBase<GameManager>
{
    public delegate void GameStateChandeDelegate(GameState newState);
    public GameStateChandeDelegate GameStateChanged;

    public delegate void TimeScaleChangedDelegate(float newTime);
    public TimeScaleChangedDelegate timeChanged;

    public LevelDataSO LevelData { get => _levelData; set => _levelData = value; }
    [SerializeField] LevelDataSO _levelData;
    public TimerController Timer { get => _timer; set => _timer = value; }
    [SerializeField] TimerController _timer;
    public WavesController Waves { get => _waves; set => _waves = value; }
    [SerializeField] WavesController _waves;
    public BuildController Build { get => _build; set => _build = value; }
    [SerializeField] BuildController _build;
    public ScoreController Score { get => _score; set => _score = value; }
    [SerializeField] ScoreController _score;
    public LevelEvaluatorController Evaluator { get => _evaluator; set => _evaluator = value; }
    public bool Tutorial { get => _tutorial; set => _tutorial = value; }

    [SerializeField] LevelEvaluatorController _evaluator;

    GameState currentState;

    [SerializeField] bool _tutorial = false;

    private void Awake()
    {

    }

    private void Start()
    {
        PrepareGame();
    }
    public void PrepareGame()
    {
        ServiceLocator.GetService<AudioManager>().PlayMainMusic("Musica 2");
        ChangeStateGame(GameState.PREPARE, 1);

        _timer.OnTimeFinished += StartGame;
        _waves.WavesFinished += FinishGame;
        _waves.EnemiesKilled += FinishGame;

        if (!_levelData)
        {
            _levelData = ServiceLocator.GetService<DataManager>().LevelData;
        }

        _waves.InitializeComponent();
        _timer.StartTimer(0.1f);
    }

    public void StartGame()
    {
        ChangeStateGame(GameState.PLAYING, 1);
        _timer.OnTimeFinished -= StartGame;

        if (!_tutorial)
        {
            _waves.SpawnWave();
        }
    }

    public void PauseGame()
    {
        ChangeStateGame(GameState.PAUSED, 0);
    }
    public void ResumeGame()
    {
        ChangeStateGame(GameState.PLAYING, 1);
    }

    public void FinishGame()
    {
        _evaluator.Evaluate();
        ChangeStateGame(GameState.FINISHED, 1);
    }

    protected override GameManager CreateService()
    {
        ServiceLocator.RegisterService(this);
        return this;
    }

    public void ChangeStateGame(GameState newState, float timeScale)
    {
        currentState = newState;
        GameStateChanged?.Invoke(currentState);
        ChangeTimeScale(timeScale);
    }

    public void ChangeTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        timeChanged?.Invoke(timeScale);
    }

    public void NextTimeScale()
    {
        switch (Time.timeScale)
        {
            case 1:
                ChangeTimeScale(2);
                break;

            case 2:
                ChangeTimeScale(3);
                break;

            case 3:
                ChangeTimeScale(1);
                break;
        }
    }

    public GameState GetCurrentState()
    {
        return currentState;
    }
}
