using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _Abdullah
{
    /// <summary>
    /// Handles email simulation on PC monitor
    /// </summary>
    public class EmailSimulation : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject emailPanel;
        [SerializeField] private TextMeshProUGUI senderText;
        [SerializeField] private TextMeshProArGUI subjectText;
        [SerializeField] private TextMeshProArGUI contentText;
        [SerializeField] private Button scamButton;
        [SerializeField] private Button legitimateButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private TextMeshProArGUI explanationText;

        [Header("Data")]
        [SerializeField] private MessageDatabase messageDatabase;

        [SerializeField] private ScoreEvent scoreEvent;

        private int currentMessageIndex = 0;
        private ScamMessageData currentMessage;

        private void Start()
        {
            if (messageDatabase == null || messageDatabase.messages.Length == 0)
            {
                Debug.LogError("Message database not assigned or empty!");
                return;
            }

            SetupButtons();
            LoadMessage(0);
        }

        private void SetupButtons()
        {
            scamButton.onClick.AddListener(() => OnDecisionMade(true));
            legitimateButton.onClick.AddListener(() => OnDecisionMade(false));
            nextButton.onClick.AddListener(LoadNextMessage);
            nextButton.gameObject.SetActive(false);
        }

        private void LoadMessage(int index)
        {
            if (index >= messageDatabase.messages.Length)
            {
                // All messages completed
                feedbackText.text = "All messages completed!";
                explanationText.text = "Great job practicing scam detection!";
                DisableDecisionButtons();
                return;
            }

            currentMessage = messageDatabase.messages[index];

            // Only show email messages
            if (currentMessage.messageType != MessageType.Email)
            {
                LoadMessage(index + 1);
                return;
            }

            currentMessageIndex = index;

            senderText.text = $"From: {currentMessage.sender}";
            subjectText.text = $"{currentMessage.subject}";
            subjectText.ConvertToArabic();
            contentText.text = currentMessage.content;
            contentText.ConvertToArabic();

            feedbackText.text = "";
            explanationText.text = "";

            EnableDecisionButtons();
            nextButton.gameObject.SetActive(false);
        }

        private void OnDecisionMade(bool playerThinksScam)
        {
            bool isCorrect = playerThinksScam == currentMessage.isScam;

            if (isCorrect)
            {
                feedbackText.text = "!ﺢﻴﺤﺻ";
                feedbackText.color = Color.green;
                scoreEvent.InvokeOnScoreIncrease();
            }
            else
            {
                feedbackText.text = "!ﺄﻄﺧ";
                feedbackText.color = Color.red;
            }

            explanationText.text = currentMessage.explanation;
            explanationText.ConvertToArabic();

            DisableDecisionButtons();
            nextButton.gameObject.SetActive(true);
        }

        private void LoadNextMessage()
        {
            LoadMessage(currentMessageIndex + 1);
        }

        private void EnableDecisionButtons()
        {
            scamButton.interactable = true;
            legitimateButton.interactable = true;
        }

        private void DisableDecisionButtons()
        {
            scamButton.interactable = false;
            legitimateButton.interactable = false;
        }

        /// <summary>
        /// Called when player looks at the monitor
        /// </summary>
        public void OnPlayerLook()
        {
            emailPanel.SetActive(true);
        }

        /// <summary>
        /// Called when player looks away from the monitor
        /// </summary>
        public void OnPlayerLookAway()
        {
            emailPanel.SetActive(false);
        }
    }
}
