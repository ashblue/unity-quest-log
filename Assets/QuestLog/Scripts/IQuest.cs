using System.Collections.Generic;

namespace CleverCrow.QuestLogs {
    public interface IQuest {
        string DisplayName { get; }
        string Description { get; }
        QuestStatus Status { get; }
        
        List<IQuestTask> Tasks { get; }
        IQuestTask ActiveTask { get; }
        
        void Setup ();
        IQuest GetCopy ();
        void NextTask ();
    }
}