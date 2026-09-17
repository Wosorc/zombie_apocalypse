using System.Reflection.Emit;
using Sandbox;

public sealed class ZombieAttack : Component
{
	[Property] private AtackZone atackZone { get; set; }
	[Property] private float Damage { get; set; }
	[Property] private float HitRate { get; set; }
	private TimeSince lastHit;

	protected override void OnUpdate()
	{
		if ( atackZone.target != null )
		{
			Atack();
		}
	}
	private void Atack()
	{
		var targetPos = atackZone.target.WorldPosition;
		var direction = targetPos - WorldPosition;

		if ( direction.LengthSquared < 80f * 80f )
		{
			var stat = atackZone.target.GetComponent<Stat>();

			if ( stat != null && lastHit > HitRate )
			{
				stat.TakeDamage( Damage );
				lastHit = 0f;
			}

			return;
		}
	}
}
