using Sandbox;
using System;
using System.Numerics;

public sealed class BoxStat : Interactable
{
	[Property] private int amout { get; set; }
	[Property] private StatType type { get; set; }
	protected override void Interact( GameObject player )
	{
		var stat = player.GetComponent<Stat>();

		if (type == StatType.Health)
		{
			stat.AddHealth( amout );
		}
		else
		{
			stat.AddArmor( amout );
		}

		GameEvents.ItemCollected( GameObject );
		GameObject.Destroy();
	}
}
