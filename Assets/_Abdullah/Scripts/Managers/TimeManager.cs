using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int maxTimer;
    [SerializeField] private TMP_Text timerTMP;
    [SerializeField] private GameEndEvent gameEndEvent;
    [SerializeField] private TimeEvent timeEvent;

    private float _currentTimer;
    private bool _isTimeEnd;

    private void Awake()
    {
        _currentTimer = maxTimer;
    }

    private void Start()
    {
        timeEvent.InvokeOnTimeStart(maxTimer);
    }

    private void Update()
    {
        if (_currentTimer <= 0 && !_isTimeEnd)
        {
            _isTimeEnd = true;
            gameEndEvent.InvokeOnGameEnd();
        }

        _currentTimer -= Time.deltaTime;
        timeEvent.InvokeOnTimeChanged(_currentTimer);
        timerTMP.text = ArabicSupport.ArabicFixer.Fix(_currentTimer.ToString("0"), false, true) + " : ﺖﻗﻮﻟا";
    }

    [ContextMenu("EndGame")]
    private void EndGame()
    {
        _isTimeEnd = true;
        gameEndEvent.InvokeOnGameEnd();
    }
}
