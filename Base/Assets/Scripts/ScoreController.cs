using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    public delegate void ScoreChangedHandler(int wave);
    public ScoreChangedHandler ScoreChangedStarted;

    [SerializeField] int currentScore;

    private void Start()
    {
        StartCoroutine(InitializeComponents());
    }

    public void AddScore(int score)
    {
        currentScore += score;
        ScoreChangedStarted?.Invoke(currentScore);
    }

    public void RemoveScore(int score)
    {
        currentScore -= score;
        ScoreChangedStarted?.Invoke(currentScore);
    }

    public bool ValidateScore(int score)
    {
        if (currentScore >= score)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    IEnumerator InitializeComponents()
    {
        yield return null;
        ScoreChangedStarted?.Invoke(currentScore);
    }
}
