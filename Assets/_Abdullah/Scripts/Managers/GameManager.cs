using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEndEvent gameEndEvent;
    [SerializeField] private ScoreEvent scoreEvent;
    [SerializeField] private TimeEvent timeEvent;
    [SerializeField] private Canvas HUDCanvas;
    [SerializeField] private Canvas endGameCanvas;
    [SerializeField] private TMP_Text currentScoreTMP;
    [SerializeField] private TMP_Text bestScoreTMP;
    [SerializeField] private TMP_Text timeTMP;
    [SerializeField] private Button restartButton;

    private int _currentScore;
    private float _currentTimer;
    private float _startTimer;

    private void OnEnable()
    {
        gameEndEvent.OnGameEnd += EndGame;
        scoreEvent.OnScoreChanged += UpdateCurrentScore;
        timeEvent.OnTimeStart += UpdateStartTime;
        timeEvent.OnTimeChanged += UpdateCurrentTime;
    }

    private void OnDisable()
    {
        gameEndEvent.OnGameEnd -= EndGame;
        scoreEvent.OnScoreChanged -= UpdateCurrentScore;
        timeEvent.OnTimeStart -= UpdateStartTime;
        timeEvent.OnTimeChanged -= UpdateCurrentTime;
    }

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        HUDCanvas.enabled = false;
        endGameCanvas.enabled = true;

        bestScoreTMP.text = ArabicSupport.ArabicFixer.Fix(PlayerPrefs.GetString("Score", "0"), false, true) + " : ﻲﻋو ىﻮﺘﺴﻣ ﻞﻀﻓا";
        currentScoreTMP.text = ArabicSupport.ArabicFixer.Fix(_currentScore.ToString("0")) + " : ﻲﻟﺎﺤﻟا ﻲﻋﻮﻟا ىﻮﺘﺴﻣ";
        timeTMP.text = ArabicSupport.ArabicFixer.Fix(GetTimeElapsed().ToString("0")) + " : ﻲﻀﻘﻨﻤﻟا ﺖﻗﻮﻟا";
    }

    private float GetTimeElapsed()
    {
        return _startTimer - _currentTimer;
    }

    private void UpdateCurrentScore(int newScore)
    {
        _currentScore = newScore;
    }

    private void UpdateStartTime(float newTime)
    {
        _startTimer = newTime;
    }

    private void UpdateCurrentTime(float newTime)
    {
        _currentTimer = newTime;
    }
}
