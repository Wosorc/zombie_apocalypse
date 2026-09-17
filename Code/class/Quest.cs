using Editor;
using Sandbox;

public class Quest
{
	public string Name { get; set; }
	public List<QuestObjective> Objectives { get; set; } = new();
	public bool Required { get; set; }
	public bool IsCompleted
	{
		get
		{
			foreach ( var objective in Objectives )
			{
				if ( !objective.IsCompleted )
				{
					return false;
				}
			}
			
			return true;
		}
	}
}