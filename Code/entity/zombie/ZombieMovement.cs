using System.Diagnostics.CodeAnalysis;
using Sandbox;
using Sandbox.ActionGraphs;

public sealed class ZombieMovement : Component
{
	[Property] private FollowZone followZone { get; set; }
	private DictionarymMovement dictionarymMovement;
	private Rigidbody rigidbody;
	protected override void OnStart()
	{
		dictionarymMovement = GetComponent<DictionarymMovement>();
		rigidbody = GetComponent<Rigidbody>();
	}
	protected override void OnUpdate()
	{
		if ( followZone.target != null )
		{
			Follow();
		}	
	}
	private void Follow()
	{
		var direction = followZone.target.WorldPosition - WorldPosition;
		var velocity = direction.Normal * dictionarymMovement.GetSettings().Speed;

		if( direction.LengthSquared > 80f * 80f )
		{
			rigidbody.Velocity = velocity;
		}
	}
}
