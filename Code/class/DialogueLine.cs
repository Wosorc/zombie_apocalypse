using Editor;
using Sandbox;

public class DialogueLine
{
	public NameNPC Name { get; set; }
	public string Text { get; set; }
	public SoundFile Sound { get; set; }
	public float TimeSound { get; set; }
	public bool EnableQuest { get; set; }
	[ShowIf( "EnableQuest", true )] public Quest Quest { get; set; }
}