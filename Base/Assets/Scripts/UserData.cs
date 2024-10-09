using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    [SerializeField] float _score;
    public float Score { get => _score; set => _score = value; }

    public UserData(float score)
    {
        _score = score;
    }
}