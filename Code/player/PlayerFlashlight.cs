using Sandbox;

public sealed class PlayerFlashlight : Component
{
	[Property] private SpotLight spotLight { get; set; }
	public void Toggle()
	{
		spotLight.Enabled = !spotLight.Enabled;
	}
}
