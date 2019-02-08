using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.QuestLogs {
    public class QuestGenerator : MonoBehaviour {
        private QuestCollection _quests = new QuestCollection();

        public List<QuestDetails> questDetails;
        public QuestPrint questPrint;
        public RectTransform output;
        
        [System.Serializable]
        public class QuestDetails {
            public QuestDefinition definition;
            public int taskIndex;
        }

        private void Start () {
            foreach (var quest in questDetails) {
                var copy = _quests.Add(quest.definition);
                for (var i = 0; i < quest.taskIndex; i++) {
                    copy.NextTask();
                }
            }

            foreach (var quest in _quests.Quests) {
                var print = Instantiate(questPrint, output);
                print.Setup(quest);
            }
        }
    }
}