public class QuestObjective
{
	public string Name { get; set; }
	public QuestType Type { get; set; }
	public string TargetTag { get; set; }
	public int CurrentProgress { get; set; }
	public int RequiredProgress { get; set; }
	public bool IsCompleted => CurrentProgress >= RequiredProgress;
}