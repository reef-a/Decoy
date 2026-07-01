using UnityEngine;

namespace _Abdullah
{
    /// <summary>
    /// Creates a sample message database with bank scam examples
    /// This is a helper script to generate the ScriptableObject
    /// </summary>
    public class SampleMessageDatabase : MonoBehaviour
    {
        [Header("Sample Data")]
        public ScamMessageData[] sampleMessages;

        private void Start()
        {
            // This is just for reference - actual data should be stored in a ScriptableObject
            // Create a MessageDatabase asset in Unity and populate it with these examples
        }

        /// <summary>
        /// Returns sample scam messages for testing
        /// </summary>
        public static ScamMessageData[] GetSampleMessages()
        {
            return new ScamMessageData[]
            {
                // EMAIL SCAMS
                new ScamMessageData
                {
                    sender = "security@bankofamerica.com",
                    subject = "URGENT: Your account has been suspended",
                    content = "Dear Customer,\n\nWe have detected suspicious activity on your account. Your account has been temporarily suspended for your security.\n\nPlease click the link below to verify your identity and restore your account:\n\nhttp://bankofamerica-secure-login.com/verify\n\nIf you do not act within 24 hours, your account will be permanently closed.\n\nSincerely,\nBank of America Security Team",
                    isScam = true,
                    explanation = "This is a phishing email. The URL is fake (bankofamerica-secure-login.com instead of bankofamerica.com). Legitimate banks never ask you to click links to verify your identity via email.",
                    messageType = MessageType.Email
                },
                new ScamMessageData
                {
                    sender = "noreply@chase.com",
                    subject = "Your monthly statement is available",
                    content = "Dear Customer,\n\nYour monthly statement for account ending in ****4521 is now available.\n\nLog in to your account at chase.com to view your statement.\n\nThank you for banking with Chase.\n\nThis is an automated message. Please do not reply.",
                    isScam = false,
                    explanation = "This is a legitimate bank notification. It directs you to the official chase.com website rather than a suspicious link, and doesn't create urgency or ask for personal information.",
                    messageType = MessageType.Email
                },
                new ScamMessageData
                {
                    sender = "irs-tax-refund@government.com",
                    subject = "You have a tax refund pending",
                    content = "Congratulations!\n\nThe IRS has calculated that you are eligible for a tax refund of $2,450.00.\n\nTo claim your refund, please provide your bank account information and social security number through the secure portal below:\n\nhttp://irs-refund-portal-secure.org/claim\n\nAct now - this offer expires in 48 hours.",
                    isScam = true,
                    explanation = "The IRS never initiates contact via email about tax refunds. The domain is fake (.org instead of .gov), and they would never ask for SSN or bank info via email.",
                    messageType = MessageType.Email
                },
                new ScamMessageData
                {
                    sender = "support@wellsfargo.com",
                    subject = "Security Alert: New device detected",
                    content = "We noticed a sign-in to your account from a new device:\n\nDevice: iPhone 14\nLocation: Los Angeles, CA\nTime: March 15, 2024 at 2:34 PM\n\nIf this was you, no action is needed. If you didn't sign in, please secure your account by changing your password at wellsfgargo.com.",
                    isScam = true,
                    explanation = "This is a phishing attempt. Notice the typo in the URL 'wellsfgargo.com' instead of 'wellsfargo.com'. Always verify the domain carefully.",
                    messageType = MessageType.Email
                },
                
                // SMS SCAMS
                new ScamMessageData
                {
                    sender = "+1 (555) 123-4567",
                    subject = "",
                    content = "ALERT: Your debit card has been locked. Call 1-800-555-0199 immediately to unlock. Ref: #BANK-SECURE",
                    isScam = true,
                    explanation = "This is a smishing (SMS phishing) attempt. Banks never send text messages asking you to call a number to unlock your card. The reference number is fake.",
                    messageType = MessageType.SMS
                },
                new ScamMessageData
                {
                    sender = "Chase Bank",
                    subject = "",
                    content = "Chase: Did you attempt a $500.00 transfer at 3:45 PM? Reply YES if authorized, NO if not. Text HELP for help, STOP to cancel.",
                    isScam = false,
                    explanation = "This is a legitimate fraud alert from Chase. It uses a short code format and allows you to reply with simple commands. It's verifying a transaction, not asking for personal info.",
                    messageType = MessageType.SMS
                },
                new ScamMessageData
                {
                    sender = "Bank of America",
                    subject = "",
                    content = "Your account is overdrawn. Please send payment to bitcoin-wallet-recovery.com to restore your account status immediately.",
                    isScam = true,
                    explanation = "Legitimate banks never ask for payments via cryptocurrency or external websites. This is a clear scam attempting to steal your money.",
                    messageType = MessageType.SMS
                },
                
                // CALL SCAMS
                new ScamMessageData
                {
                    sender = "Unknown",
                    subject = "",
                    content = "Hello, this is calling from the fraud department at your bank. We've detected unauthorized transactions on your account. Can you please verify your social security number and date of birth so we can secure your account?",
                    isScam = true,
                    explanation = "Legitimate bank fraud departments will never ask for your SSN or date of birth over the phone. They already have this information. This is a vishing (voice phishing) attempt.",
                    messageType = MessageType.Call
                },
                new ScamMessageData
                {
                    sender = "Citibank",
                    subject = "",
                    content = "Hi, this is Sarah from Citibank's security team. We're calling to verify a recent transaction on your card. Can you confirm the last 4 digits of the card you used for a $75 purchase at Amazon today?",
                    isScam = false,
                    explanation = "This could be a legitimate fraud verification call. The bank is asking you to confirm information they should already have, not asking you to provide new sensitive information like your full card number or SSN.",
                    messageType = MessageType.Call
                },
                new ScamMessageData
                {
                    sender = "IRS",
                    subject = "",
                    content = "This is the Internal Revenue Service. We are filing a lawsuit against you for tax fraud. You must call us back immediately at 202-555-0123 to resolve this matter or face arrest.",
                    isScam = true,
                    explanation = "The IRS never calls to threaten arrest or lawsuits. All official IRS communication is done through mail. This is a common scare tactic used in IRS impersonation scams.",
                    messageType = MessageType.Call
                }
            };
        }
    }
}
