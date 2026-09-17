using Sandbox;

public sealed class PlayerCamera : Component
{
	private PlayerMovement playerMovement;
	[Property] public GameObject camera { get; private set; }
	[Property] private float sensitivity { get; set; }
	private float yaw;
	private float pitch;
	protected override void OnStart()
	{
		playerMovement = GetComponent<PlayerMovement>();
		playerMovement.OnHeightChanged += SetHeight;
	}

	protected override void OnDestroy()
	{
		if ( playerMovement != null )
		{
			playerMovement.OnHeightChanged -= SetHeight;
		}
	}

	public void SetHeight( float height )
	{
		camera.LocalPosition = new Vector3( 0, 0, height );
	}

	protected override void OnUpdate()
	{
		var delta = Input.MouseDelta;

		yaw -= delta.x * sensitivity;
		pitch += delta.y * sensitivity;

		pitch = pitch.Clamp( -89f, 89f );

		GameObject.WorldRotation = Rotation.FromYaw( yaw );
		camera.LocalRotation = Rotation.FromPitch( pitch );
	}
}
