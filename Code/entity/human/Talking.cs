using Sandbox;

public sealed class Talking : Interactable
{
	private DialogueController dialogueController;
	protected override void OnStart()
	{
		dialogueController = GetComponent<DialogueController>();
	}
	protected override void Interact( GameObject player )
	{
		dialogueController.StartDialogue( player );

		GameEvents.NpcTalked( GameObject );
	}
}
