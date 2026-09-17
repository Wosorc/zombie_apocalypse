using Sandbox;

public sealed class ZombieLook : Component
{
	[Property] private FollowZone followZone { get; set; }
	protected override void OnFixedUpdate()
	{
		if ( followZone.target != null )
		{
			Look();
		}
	}
	private void Look()
	{
		//Вгзляд
		var targetPos = followZone.target.WorldPosition;
		var direction = targetPos - WorldPosition;

		if( direction.LengthSquared > 30f * 30f )
		{
			direction = direction.WithZ(0);
			WorldRotation = Rotation.LookAt( direction );
		}
	}
}
