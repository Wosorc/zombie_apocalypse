using Sandbox;

public sealed class Zone : Component, Component.ITriggerListener
{
	[Property] private bool EnableDamage { get; set; }
	[Property] private bool EnableReach { get; set; }
 	[Property, ShowIf( "EnableDamage", true )] private float Damage { get; set; }

	public void OnTriggerEnter( Collider colliderOther )
	{
		var gameObjectOther  = colliderOther.GameObject;

		if ( EnableDamage )
		{
			gameObjectOther.GetComponent<Stat>()?.TakeDamage( Damage );
		}

		if ( EnableReach )
		{
			GameEvents.LocationReached( GameObject );
		}
	}
	
}
