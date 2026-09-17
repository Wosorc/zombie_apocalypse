using Sandbox;
using System.Collections.Generic;

public sealed class QuestController : Component
{
	[Property] public List<Quest> Quests { get; private set; } = new();
	public int Revision { get; private set; }
	
	public void AddQuest( Quest quest )
	{
		Quests.Add( quest );
		Revision++;

		Log.Info( $"Квест добавлен: {quest.Name}" );
	}

	protected override void OnStart()
	{
		GameEvents.OnEnemyKilled += OnEnemyKilled;
		GameEvents.OnItemCollected += OnItemCollected;
		GameEvents.OnNpcTalked += OnNpcTalked;
		GameEvents.OnLocationReached += OnLocationReached;
	}

	protected override void OnDestroy()
    {
        GameEvents.OnEnemyKilled -= OnEnemyKilled;
        GameEvents.OnItemCollected -= OnItemCollected;
        GameEvents.OnNpcTalked -= OnNpcTalked;
        GameEvents.OnLocationReached -= OnLocationReached;
    }

	public void CompleteQuest( Quest quest )
	{
		Revision++;

		Log.Info( $"Квест выполнен: {quest.Name}" );
	}

	public void UpdateObjective( QuestType type, GameObject target )
	{
		foreach ( var quest in Quests )
		{
			foreach (var objective in quest.Objectives )
			{
				if ( objective.IsCompleted )
				{
					continue;
				}
				
				if ( objective.Type != type )
				{
					continue;
				}

				if ( !target.Tags.Has( objective.TargetTag ) )
				{
					continue;
				}

				objective.CurrentProgress ++;

				Log.Info( $"{quest.Name}: {objective.CurrentProgress}/{objective.RequiredProgress}" );

				if ( quest.IsCompleted )
				{
					CompleteQuest( quest );
				}
			}
		}
	}

	private void OnEnemyKilled( GameObject target )
	{
		UpdateObjective( QuestType.Kill, target );
	}

	private void OnItemCollected( GameObject target )
	{
		UpdateObjective( QuestType.Collect, target );
	}

    private void OnNpcTalked( GameObject target )
    {
        UpdateObjective( QuestType.Talk, target );
    }

    private void OnLocationReached( GameObject target )
    {
        UpdateObjective( QuestType.Reach, target );
    }
}
