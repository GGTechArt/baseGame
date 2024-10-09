using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public delegate void TimerFinishedHandler();
    public TimerFinishedHandler OnTimeFinished;

    float currenTime;

    // Update is called once per frame
    void Update()
    {
        if (currenTime > 0)
        {
            currenTime -= Time.deltaTime;

            if (currenTime <= 0)
            {
                OnTimeFinished?.Invoke();
            }
        }
    }

    public void StartTimer(float newTime)
    {
        currenTime = newTime;
    }

    public void FinishTimer()
    {
        currenTime = 0;
    }
}
