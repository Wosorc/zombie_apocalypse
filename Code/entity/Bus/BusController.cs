using Sandbox;

public sealed class BusController : Component, IMovingPlatform
{
	[Property] private List<GameObject> points { get; set; } = new();
	[Property] private float speed = 300f;
	private int currentPoint;
	private Vector3 lastPosition;
	private Vector3 deltaPosition;
	public Vector3 DeltaPosition => deltaPosition;
	protected override void OnFixedUpdate()
	{
		lastPosition = WorldPosition;

		if ( currentPoint >= points.Count )
		{
			deltaPosition = Vector3.Zero;
			return;
		}

		var target = points[currentPoint];

		var distance = WorldPosition.Distance( target.WorldPosition );
		
		if ( distance <= speed * Time.Delta )
		{
			WorldPosition = target.WorldPosition;
			currentPoint ++;
		}
		else
		{
			var direction = ( target.WorldPosition - WorldPosition ).Normal;
			WorldPosition += direction * speed * Time.Delta;
		}

		deltaPosition = WorldPosition - lastPosition;
	}
}
