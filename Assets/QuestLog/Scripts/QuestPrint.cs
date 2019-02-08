using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CleverCrow.QuestLogs {
    public class QuestPrint : MonoBehaviour {
        public Text title;
        public Text status;
        public Text description;
        public Text task;
        
        public void Setup (IQuest quest) {
            title.text = quest.DisplayName;
            status.text = $"Status: {quest.Status.ToString()}";
            description.text = quest.Description;
            task.text = quest.ActiveTask.Description;
        }
    }
}
