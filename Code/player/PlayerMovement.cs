using Sandbox;
using System;

public sealed class PlayerMovement : Component
{
	[Property] public bool CanCrouching = true;
	[Property] public bool CanWalkin = true;
	[Property] public bool CanJumping = true;
	[Property] public bool CanRunning = true;
	
	private DictionarymMovement dictionarymMovement;
	private CapsuleCollider collider;
	private Rigidbody rigidbody;
	private float speed;
	private Vector3 move;
	private IMovingPlatform currentPlatform;
	
	public event Action<float> OnHeightChanged;
	
	protected override void OnStart()
	{
		dictionarymMovement = GetComponent<DictionarymMovement>();
		collider = GetComponent<CapsuleCollider>();
		rigidbody = GetComponent<Rigidbody>();
	}
	
	protected override void OnFixedUpdate()
	{
		IsPlatform();
		HandleCrouch();
		HandleMovementInput();
		ApplyMovement();
	}
	
	private void HandleCrouch()
	{
		if ( Input.Down( "duck" ) && CanCrouching )
		{
			dictionarymMovement.SetMovementType( MovementType.Crouching );
			ToggleCrouch( dictionarymMovement.GetSettings().Height, dictionarymMovement.GetSettings().Speed );
		}
		else if ( CanStandUp() )
		{
			dictionarymMovement.SetMovementType( MovementType.Walking );
			ToggleCrouch( dictionarymMovement.GetSettings().Height, dictionarymMovement.GetSettings().Speed );
			if ( Input.Down( "run" ) && CanRunning )
			{
				dictionarymMovement.SetMovementType( MovementType.Running );
				speed = dictionarymMovement.GetSettings().Speed;
			}
		}
	}
	
	private void HandleMovementInput()
	{
		move = Vector3.Zero;

		if ( CanWalkin )
		{
			if ( Input.Down( "forward" ) ) move += WorldRotation.Forward;
			if ( Input.Down( "backward" ) ) move += WorldRotation.Backward;
			if ( Input.Down( "right" ) ) move += WorldRotation.Right;
			if ( Input.Down( "left" ) ) move += WorldRotation.Left;
		}

		move = move.Normal;

		if ( CanJumping )
		{
			if ( Input.Down( "jump" ) && IsGrounded() ) rigidbody.Velocity = rigidbody.Velocity.WithZ( dictionarymMovement.JumpHeight );
		}
	}
	
	private void ApplyMovement()
	{
		var velocity = rigidbody.Velocity;

		if (currentPlatform != null)
		{
			WorldPosition += currentPlatform.DeltaPosition;
		}

		velocity.x = move.x * speed;
		velocity.y = move.y * speed;

		rigidbody.Velocity = velocity;
	}
	
	private bool CanStandUp()
	{
		var start = WorldPosition + new Vector3( 0, 0, dictionarymMovement.GetSettings().Height );
		var end = WorldPosition + new Vector3( 0, 0, dictionarymMovement.Settings[MovementType.Walking].Height );
		
		// DebugOverlay.Sphere( new Sphere( start, collider.Radius ) );
		// DebugOverlay.Sphere( new Sphere( end, collider.Radius ) );

		var tr = Scene.Trace
			.Sphere( collider.Radius, start, end )
			.IgnoreGameObject( GameObject )
			.Run();
		return !tr.Hit;
	}
	
	private void ToggleCrouch( float height, float speed )
	{
		collider.End = new Vector3( 0, 0, height );
		OnHeightChanged?.Invoke( height );
		this.speed = speed;
	}
	
	private bool IsGrounded()
	{
		var center = WorldPosition + collider.Start;

		var tr = Scene.Trace
			.Sphere( collider.Radius, center, center )
			.IgnoreGameObject( GameObject )
			.Run();

		return tr.Hit;
	}
	
	private void IsPlatform()
	{
		var center = WorldPosition + collider.Start;

		// DebugOverlay.Sphere( new Sphere( center, collider.Radius ) );

		var tr = Scene.Trace
			.Sphere( collider.Radius, center, center )
			.IgnoreGameObject( GameObject )
			.Run();
		
		if ( tr.Hit )
		{
			currentPlatform = tr.GameObject?.GetComponent<IMovingPlatform>();
		}
		else
		{
			currentPlatform = null;
		}
	}
}
