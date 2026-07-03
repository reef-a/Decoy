using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Events/Score Increase")]
public class ScoreEvent : ScriptableObject
{
    public event Action OnScoreIncrease;
    public event Action<int> OnScoreChanged;

    public void InvokeOnScoreIncrease()
    {
        OnScoreIncrease?.Invoke();
    }

    public void InvokeOnScoreChanged(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);
    }
}
