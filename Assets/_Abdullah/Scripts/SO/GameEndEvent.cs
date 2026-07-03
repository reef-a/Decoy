using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Events/End Game")]
public class GameEndEvent : ScriptableObject
{
    public event Action OnGameEnd;

    public void InvokeOnGameEnd()
    {
        OnGameEnd?.Invoke();
    }
}
