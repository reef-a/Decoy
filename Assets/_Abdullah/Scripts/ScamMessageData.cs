using UnityEngine;

namespace _Abdullah
{
    /// <summary>
    /// Data structure for scam/legitimate messages
    /// </summary>
    [System.Serializable]
    public class ScamMessageData
    {
        public string sender;
        public string subject; // For emails
        public string content;
        public bool isScam;
        public string explanation; // Explanation of why it's a scam or legitimate
        public MessageType messageType;
    }

    public enum MessageType
    {
        Email,
        SMS,
        Call
    }
}
