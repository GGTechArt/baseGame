using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public float Score { get => _score; set => _score = value; }
    [SerializeField] float _score;

    public UserData(float score)
    {
        _score = score;
    }
}
