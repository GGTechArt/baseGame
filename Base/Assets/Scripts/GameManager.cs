using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ServiceInstallerBase<GameManager>
{
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

    private void Awake()
    {

    }

    private void Start()
    {
        PrepareGame();
    }
    public void PrepareGame()
    {
        _timer.OnTimeFinished += StartGame;
        _waves.WavesFinished += FinishGame;

        _waves.InitializeComponent();
        _timer.StartTimer(1f);
    }

    public void StartGame()
    {
        _timer.OnTimeFinished -= StartGame;

        _waves.SpawnWave();
    }

    public void PauseGame()
    {

    }
    public void ResumeGame()
    {

    }

    public void FinishGame()
    {

    }

    protected override GameManager CreateService()
    {
        ServiceLocator.RegisterService(this);
        return this;
    }
}
