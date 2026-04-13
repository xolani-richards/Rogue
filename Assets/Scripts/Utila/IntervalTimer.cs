using System;
using System.Collections.Generic;
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
        }

        public void Start()
        {
            if(isRunning) return;
            handle = Timing.RunCoroutine(Timer());
        }

        public void Stop ()
        {
            Timing.KillCoroutines(handle);
        }

        IEnumerator<float> Timer ()
        {
            while(duration < 0f)
            {
                yield return Timing.WaitForSeconds(tickInterval);
                duration -= tickInterval;
                OnInterval?.Invoke();
            }
            if(duration <= 0) OnTimerStop?.Invoke();
        }
        
    }
}