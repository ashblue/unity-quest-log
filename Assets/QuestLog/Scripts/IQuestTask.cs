namespace CleverCrow.QuestLogs {
    public interface IQuestTask {
        QuestStatus Status { get; set; }
        string Description { get; }
    }
}