using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudUIController : MonoBehaviour
{
    [SerializeField] HudUIHandler hud;
    [SerializeField] GameOverUIHandler gameOver;
    [SerializeField] WinUIHandler win;
    [SerializeField] PauseUIHandler pause;

    GameManager manager;
    private void Start()
    {
        manager = ServiceLocator.GetService<GameManager>();

        manager.GameStateChanged += GameStateChanged;
    }

    public void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.FINISHED:
                OpenFinishedPanel();
                break;

            case GameState.PAUSED:
                pause.Show();
                break;

            case GameState.PLAYING:
                pause.Hide();
                break;
        }
    }

    public void OpenFinishedPanel()
    {
        if (manager.Evaluator.GetStars() > 0)
        {
            win.Show();
        }

        else
        {
            gameOver.Show();
        }
    }
}
