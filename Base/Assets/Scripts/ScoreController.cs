using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    [SerializeField] int currentScore;

    public void AddScore(int score)
    {
        currentScore += score;
    }

    public void RemoveScore(int score)
    {
        currentScore -= score;
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
}
