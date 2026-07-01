using UnityEngine;

namespace _Abdullah
{
    /// <summary>
    /// ScriptableObject for storing message databases
    /// </summary>
    [CreateAssetMenu(fileName = "MessageDatabase", menuName = "Abdullah/Message Database")]
    public class MessageDatabase : ScriptableObject
    {
        public ScamMessageData[] messages;
    }
}
