#if UNITY_EDITOR

using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace _Abdullah
{
    /// <summary>
    /// Imports the generated JSON file into a MessageDatabase ScriptableObject.
    /// Put this script inside Assets/Editor.
    /// </summary>
    public static class MessageDatabaseJsonImporter
    {
        [System.Serializable]
        private class MessageDatabaseJson
        {
            public ScamMessageData[] messages;
        }

        [MenuItem("Tools/Bank Scam/Import Messages JSON")]
        public static void ImportMessages()
        {
            string jsonPath = EditorUtility.OpenFilePanel(
                "Choose phishing_messages.json",
                Application.dataPath,
                "json"
            );

            if (string.IsNullOrWhiteSpace(jsonPath))
            {
                return;
            }

            string json = File.ReadAllText(jsonPath, Encoding.UTF8);

            MessageDatabaseJson imported =
                JsonUtility.FromJson<MessageDatabaseJson>(json);

            if (imported == null ||
                imported.messages == null ||
                imported.messages.Length == 0)
            {
                EditorUtility.DisplayDialog(
                    "Import failed",
                    "The JSON file does not contain any messages.",
                    "OK"
                );

                return;
            }

            string assetPath = EditorUtility.SaveFilePanelInProject(
                "Save Message Database",
                "MessageDatabase",
                "asset",
                "Choose where to save the MessageDatabase asset."
            );

            if (string.IsNullOrWhiteSpace(assetPath))
            {
                return;
            }

            MessageDatabase database =
                AssetDatabase.LoadAssetAtPath<MessageDatabase>(assetPath);

            if (database == null)
            {
                database =
                    ScriptableObject.CreateInstance<MessageDatabase>();

                AssetDatabase.CreateAsset(database, assetPath);
            }

            database.messages = imported.messages;

            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Selection.activeObject = database;
            EditorGUIUtility.PingObject(database);

            EditorUtility.DisplayDialog(
                "Import complete",
                $"Imported {database.messages.Length} messages successfully.",
                "OK"
            );
        }
    }
}

#endif