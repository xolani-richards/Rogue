using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace ImprovedTimers
{
    public class IntervalTimer
    {
        public Action OnTimerStop;
        public Action OnInterval;

        CoroutineHandle handle;
        float duration = 0;
        float tickInterval = 0;
        bool isRunning = false;

        public IntervalTimer(float duration, float tickInterval)
        {
            this.duration = duration;
            this.tickInterval = tickInterval;
            this.isRunning = false;
        }

        public void Start()
        {
            if(isRunning) return;
            handle = Timing.RunCoroutine(Timer());
        }

        public void Stop ()
        {
            Timing.KillCoroutines(handle);
            isRunning = false;
        }

        IEnumerator<float> Timer ()
        {
            Debug.Log($"Timer starting: {duration}:{tickInterval}");
            isRunning = true;

            while(duration > 0f)
            {
                yield return Timing.WaitForSeconds(tickInterval);
                duration -= tickInterval;
                OnInterval?.Invoke();
            }
            if(duration <= 0) OnTimerStop?.Invoke();
            isRunning = false;
        }
        
    }
}