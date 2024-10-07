using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ServiceInstallerBase<GameManager>
{
    public LevelData LevelData { get => _levelData; set => _levelData = value; }
    [SerializeField] LevelData _levelData;
    public TimerController Timer { get => _timer; set => _timer = value; }
    [SerializeField] TimerController _timer;
    public WavesController Waves { get => _waves; set => _waves = value; }
    [SerializeField] WavesController _waves;
    public BuildController Build { get => _build; set => _build = value; }
    [SerializeField] BuildController _build;

    private void Awake()
    {
        Debug.Log("Inicia la escena");

     
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
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
