using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int quanta;
    public int startQuanta = 400;

    public static int Lives;
    public int startLives = 20;
    private void Start()
    {
        quanta = startQuanta;
        Lives = startLives;
    }
}
