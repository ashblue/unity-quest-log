using System.Collections.Generic;

namespace CleverCrow.QuestLogs {
    public class QuestCollection {
        private Dictionary<IQuest, IQuest> _questInstances = new Dictionary<IQuest, IQuest>();
        
        public List<IQuest> Quests = new List<IQuest>();
        
        public void Add (IQuest quest) {
            var questCopy = quest.GetCopy();
            questCopy.Setup();
            Quests.Add(questCopy);
            _questInstances[quest] = questCopy;
        }

        public IQuest Get (IQuest quest) {
            return _questInstances[quest];
        }
    }
}
