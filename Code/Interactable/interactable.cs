using Sandbox;
using System.Numerics;

public abstract class Interactable : Component
{
	[Property] public string PromptMessage;

	public void BaseInteract( GameObject player )
	{
		Interact( player );
	}
	protected abstract void Interact( GameObject player );
}
