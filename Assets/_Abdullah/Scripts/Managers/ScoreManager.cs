using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreEvent scoreEvent;
    [SerializeField] private TMP_Text scoreTMP;

    private int _currentScore;

    private void OnEnable()
    {
        scoreEvent.OnScoreIncrease += IncreaseScore;
    }

    private void OnDisable()
    {
        scoreEvent.OnScoreIncrease -= IncreaseScore;
    }

    private void IncreaseScore()
    {
        _currentScore++;
        scoreEvent.InvokeOnScoreChanged(_currentScore);
        scoreTMP.text = ArabicSupport.ArabicFixer.Fix(_currentScore.ToString("0"), false, true) + " : ﻲﻋﻮﻟا ىﻮﺘﺴﻣ";

        if (_currentScore > int.Parse(PlayerPrefs.GetString("Score", "0")))
        {
            PlayerPrefs.SetString("Score", _currentScore.ToString("0"));
        }
    }
}
