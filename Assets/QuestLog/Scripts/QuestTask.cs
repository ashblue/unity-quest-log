using UnityEngine;

namespace CleverCrow.QuestLogs {
    [System.Serializable]
    public class QuestTask : ScriptableObject, IQuestTask {
        [SerializeField]
        [TextArea]
        private string _text;
        
        public QuestStatus Status { get; set; }
        public string Description => _text;
    }
}
