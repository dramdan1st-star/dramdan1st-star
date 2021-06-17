// Created by Tomy
// Simple Running StopWatch
// Call the IEnumerator from any Monobehaviour class to run

using System;

namespace Meta.Tools
{
    public class StopWatch
    {
        public static float runTime;
        private static bool _isRunning;
        
        public static System.Collections.IEnumerator RunDecrement(int time, UnityEngine.UI.Text uiText, Action onFinish)
        {
            runTime         = time;
            _isRunning      = true;

            while (_isRunning && runTime > 0)
            {
                runTime -= UnityEngine.Time.deltaTime;

                SetTimeText(runTime, uiText);

                yield return null;
            }

            onFinish?.Invoke();
        }

        public static System.Collections.IEnumerator RunIncrement(UnityEngine.UI.Text uiText, Action<float> onFinish)
        {
            runTime         = 0;
            _isRunning      = true;

            while (_isRunning)
            {
                runTime += UnityEngine.Time.deltaTime;

                SetTimeText(runTime, uiText);

                yield return null;
            }

            onFinish?.Invoke(runTime);
        }

        public static void Stop()
        {
            _isRunning = false;
        }

        public static void SetTimeText(float runTime, UnityEngine.UI.Text uiText)
        {
            if (uiText != null)
            {
                string timeText = GetRuntimeText(runTime);
                uiText.text = timeText;
            }
        }

        public static string GetRuntimeText(float runTime)
        {
            int minute = (int)runTime / 60;
            int second = (int)runTime % 60;

            string timeText = (minute < 10 ? ("0" + minute) : minute.ToString())
                                + ":"
                                + (second < 10 ? ("0" + second) : second.ToString());

            return timeText;
        }
    }
}