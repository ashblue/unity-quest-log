using UnityEngine;

namespace CleverCrow.QuestLogs {
    [System.Serializable]
    public class QuestTask : IQuestTask {
        [SerializeField]
        private string _text;
        
        public QuestStatus Status { get; set; }
        public string Description => _text;
    }
}
