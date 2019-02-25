using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.QuestLogs {
    [CreateAssetMenu(menuName = "Clever Crow/Quests/Definition", fileName = "Quest")]
    public class QuestDefinition : ScriptableObject, IQuest {
        private int _taskPointer;

        [SerializeField]
        private string _displayName;

        [SerializeField]
        [TextArea] 
        private string _description;

//        [HideInInspector]
        [SerializeField]
        private List<QuestTask> _tasks = new List<QuestTask>();

        public string DisplayName => _displayName;
        public string Description => _description;
        public QuestStatus Status { get; private set; } = QuestStatus.Ongoing;
        public List<IQuestTask> Tasks { get; } = new List<IQuestTask>();
        public IQuestTask ActiveTask => Tasks[_taskPointer];

        public void Setup () {
            _tasks.ForEach(t => Tasks.Add(t));
        }
        
        public IQuest GetCopy () {
            return Instantiate(this);
        }

        public void NextTask () {
            ActiveTask.Status = QuestStatus.Success;
            
            if (_taskPointer + 1 >= Tasks.Count) {
                Status = QuestStatus.Success;
                return;
            }
            
            _taskPointer = Mathf.Min(_taskPointer + 1, Tasks.Count - 1);
            ActiveTask.Status = QuestStatus.Ongoing;
        }
    }
}
