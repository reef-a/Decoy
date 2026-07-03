using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Events/Time")]
public class TimeEvent : ScriptableObject
{
    public event Action<float> OnTimeStart;
    public event Action<float> OnTimeChanged;

    public void InvokeOnTimeStart(float newTime)
    {
        OnTimeStart?.Invoke(newTime);
    }

    public void InvokeOnTimeChanged(float newTime)
    {
        OnTimeChanged?.Invoke(newTime);
    }
}
