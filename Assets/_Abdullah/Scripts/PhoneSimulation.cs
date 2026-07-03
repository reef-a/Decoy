using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _Abdullah
{
    /// <summary>
    /// Handles SMS and call simulation on phone
    /// </summary>
    public class PhoneSimulation : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject phonePanel;
        [SerializeField] private TextMeshProUGUI senderText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private TextMeshProUGUI callStatusText;
        [SerializeField] private Button scamButton;
        [SerializeField] private Button legitimateButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button answerCallButton;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private TextMeshProUGUI explanationText;

        [Header("Data")]
        [SerializeField] private MessageDatabase messageDatabase;

        [SerializeField] private ScoreEvent scoreEvent;

        private int currentMessageIndex = 0;
        private ScamMessageData currentMessage;
        private bool isCallActive = false;

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
            answerCallButton.onClick.AddListener(OnCallAnswered);

            nextButton.gameObject.SetActive(false);
            answerCallButton.gameObject.SetActive(false);
        }

        private void LoadMessage(int index)
        {
            if (index >= messageDatabase.messages.Length)
            {
                feedbackText.text = "All messages completed!";
                explanationText.text = "Great job practicing scam detection!";
                DisableDecisionButtons();
                return;
            }

            currentMessage = messageDatabase.messages[index];

            // Only show SMS and Call messages
            if (currentMessage.messageType == MessageType.Email)
            {
                LoadMessage(index + 1);
                return;
            }

            currentMessageIndex = index;

            senderText.text = currentMessage.sender;
            callStatusText.text = "";

            if (currentMessage.messageType == MessageType.Call)
            {
                messageText.text = "Incoming call...";
                isCallActive = true;
                answerCallButton.gameObject.SetActive(true);
                scamButton.gameObject.SetActive(false);
                legitimateButton.gameObject.SetActive(false);
            }
            else // SMS
            {
                messageText.text = currentMessage.content;
                isCallActive = false;
                answerCallButton.gameObject.SetActive(false);
                scamButton.gameObject.SetActive(true);
                legitimateButton.gameObject.SetActive(true);
            }

            feedbackText.text = "";
            explanationText.text = "";
            nextButton.gameObject.SetActive(false);
        }

        private void OnCallAnswered()
        {
            messageText.text = currentMessage.content;
            answerCallButton.gameObject.SetActive(false);
            scamButton.gameObject.SetActive(true);
            legitimateButton.gameObject.SetActive(true);
            callStatusText.text = "Call connected";
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
        /// Called when player looks at the phone
        /// </summary>
        public void OnPlayerLook()
        {
            phonePanel.SetActive(true);
        }

        /// <summary>
        /// Called when player looks away from the phone
        /// </summary>
        public void OnPlayerLookAway()
        {
            phonePanel.SetActive(false);
        }
    }
}
