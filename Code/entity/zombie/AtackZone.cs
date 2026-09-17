using Sandbox;

public sealed class AtackZone : Component
{
	public GameObject target { get; private set; }
	private CapsuleCollider zone;
	protected override void OnStart()
	{
		zone = GetComponent<CapsuleCollider>();

		zone.OnTriggerEnter += OnTriggerEnter;
		zone.OnTriggerExit += OnTriggerExit;
	}
	private void OnTriggerEnter( Collider colliderOther )
	{
		var other = colliderOther.GameObject;

		if ( other.Tags.Has( "player" ) )
		{
			target = other;
			Log.Info( "игрок вошёл в зону атаки" );
		}

	}
	private void OnTriggerExit( Collider colliderOther )
	{
		var other = colliderOther.GameObject;

		if ( other == target )
		{
			target = null;
			Log.Info( "игрок вышел из зоны атаки" );
		}
	}
}
