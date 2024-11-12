using System;
using System.Collections;
using UnityEngine;

public static class CoroutineExtension
{
    public static Coroutine InvokeAction(this MonoBehaviour invokedOn, Action action, float time,
        bool useRealTime = false)
    {
        if (time <= 0f)
        {
            action();
            return null;
        }

        return invokedOn.StartCoroutine(WaitForSecondsRoutine(action, time, useRealTime));
    }

    private static IEnumerator WaitForSecondsRoutine(Action actionCallback, float time, bool useRealTime = false)
    {
        if (useRealTime)
        {
            yield return new WaitForSecondsRealtime(time);
        }
        else
        {
            yield return new WaitForSeconds(time);
        }

        actionCallback?.Invoke();
    }
}
