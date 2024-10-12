using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] LevelEvaluatorController _evaluator;

    GameState currentState;

    private void Awake()
    {

    }

    private void Start()
    {
        PrepareGame();
    }
    public void PrepareGame()
    {
        ChangeStateGame(GameState.PREPARE);

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
        ChangeStateGame(GameState.PLAYING);

        _timer.OnTimeFinished -= StartGame;

        _waves.SpawnWave();
    }

    public void PauseGame()
    {
        ChangeStateGame(GameState.PAUSED);
    }
    public void ResumeGame()
    {
        ChangeStateGame(GameState.PLAYING);
    }

    public void FinishGame()
    {
        _evaluator.Evaluate();
        ChangeStateGame(GameState.FINISHED);
    }

    protected override GameManager CreateService()
    {
        ServiceLocator.RegisterService(this);
        return this;
    }

    public void ChangeStateGame(GameState newState)
    {
        currentState = newState;
        GameStateChanged?.Invoke(currentState);
    }
}
