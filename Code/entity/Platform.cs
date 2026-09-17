using Sandbox;

public sealed class Platform : Component, IMovingPlatform, Component.ICollisionListener
{
	[Property] private List<GameObject> points { get; set; } = new();
    [Property] private bool reverse { get; set; }
	[Property] private float speed { get; set; } = 300f;
    [Property] private bool moveOnStart { get; set; }
    [Property] private bool moveOnCollision { get; set; } 
    private bool moving = false;
	private int currentPoint;
	private Vector3 lastPosition;
	private Vector3 deltaPosition;
	public Vector3 DeltaPosition => deltaPosition;

	protected override void OnStart()
	{
		if ( moveOnStart )
        {
            moving = true;
        }
	}

    public void OnCollisionStart( Collision collision )
    {
        if ( moveOnCollision )
        {
            moving = true;
        }
    }

	protected override void OnFixedUpdate()
	{
        if ( moving )
        {
            Move();
        }
	}

    private void Move()
    {
        lastPosition = WorldPosition;

		if ( currentPoint >= points.Count )
		{
            if ( reverse )
            {
                points.Reverse();
                currentPoint = 0;
                return;
            }

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
