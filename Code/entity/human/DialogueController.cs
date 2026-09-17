using Sandbox;

public sealed class DialogueController : Component
{
	[Property] private List<Dialogue> dialogue { get; set; } = new();
	private int currentDialogue = 0;
	private int currentDialogueLine = 0;
	private TimeSince lineTime = 0f;
	private bool isTalking = false;
	private QuestController questController = null;
	public int Revision { get; private set; }
	public string CurrentName
	{
		get
		{
			if ( !isTalking )
			{
				return null;
			}

			return dialogue[currentDialogue].DialogueLine[currentDialogueLine].Name.ToString();
		}
	}
	public string CurrentText
	{
		get
		{
			if ( !isTalking )
			{
				return null;
			}

			return dialogue[currentDialogue].DialogueLine[currentDialogueLine].Text;
		}
	}
	protected override void OnUpdate()
	{
		if( !isTalking )
		{
			return;
		}

		if( lineTime >= dialogue[currentDialogue].DialogueLine[currentDialogueLine].TimeSound )
		{
			currentDialogueLine ++;

			if( currentDialogueLine >= dialogue[currentDialogue].DialogueLine.Count )
			{
				EndDialogue();
				return;
			}

			PlayLine();
		}
	}

	public void StartDialogue( GameObject player )
	{
		if ( isTalking || currentDialogue >= dialogue.Count )
		{
			return;
		}

		questController = player.GetComponent<QuestController>();
		isTalking = true;
		currentDialogueLine = 0;

		var hud = player.GetComponentInChildren<PlayerHud>();

		if ( hud != null )
		{
			hud.SetDialogue( this );
		}

		Log.Info( $"Диалог - {dialogue[currentDialogue].Name} начат" );

		PlayLine();

		Revision++;
	}
	private void EndDialogue()
	{
		questController = null;
		isTalking = false;
		currentDialogueLine = 0;

		Log.Info( $"Диалог - {dialogue[currentDialogue].Name} законьчен" );

		currentDialogue++;

		Revision++;
	}

	private void PlayLine()
	{
		var line = dialogue[currentDialogue].DialogueLine[currentDialogueLine];

		//line.TimeSound = line.Sound.Duration;

		Log.Info( $"{line.Name}: {line.Text}" );

		if ( line.Sound != null )
		{
			var sound = Sound.PlayFile( line.Sound, 1f, 1f, 0f, 0f );

			sound.Parent = GameObject;
			sound.Position = WorldPosition;
			sound.FollowParent = true;
		}

		if ( line.EnableQuest )
		{
			questController.AddQuest( line.Quest );
		}

		lineTime = 0f;

		Revision++;
	}
}
