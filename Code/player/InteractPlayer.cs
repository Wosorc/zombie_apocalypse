using Sandbox;

public sealed class InteractPlayer : Component
{
	[Property] private float distance = 3f;
	[Property] private PlayerHud hud { get; set; }
	private Component camera;
	
	protected override void OnStart()
	{
		camera = Components.GetInChildren<CameraComponent>();
	}
	
	protected override void OnUpdate()
	{
		var start = camera.WorldPosition;
		var end = start + camera.WorldRotation.Forward * distance;

		DebugOverlay.Line( start, end, Color.Red );

		var rt = Scene.Trace
			.Ray( start, end )
			.IgnoreGameObject( GameObject )
			.Run();

		hud.PromptText = "";

		if ( rt.Hit )
		{
			var interactable = rt.GameObject.GetComponent<Interactable>();
			
			if ( interactable != null )
			{
				hud.PromptText = interactable.PromptMessage;

				if (Input.Pressed("Use"))
				{
					interactable.BaseInteract( GameObject );
				}
			}
		}
	}
}
