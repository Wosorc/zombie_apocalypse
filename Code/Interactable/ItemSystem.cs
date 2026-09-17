using Sandbox;

public sealed class ItemSystem : Interactable
{
	[Property] public string ItemName { get; private set; }
	[Property] public Texture Ico { get; private set; }
	
	private Rigidbody _rigidbody;
	private BoxCollider _collider;
	private ModelRenderer _model;

	protected override void OnStart()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<BoxCollider>();
		_model = GetComponent<ModelRenderer>();
	}

	public void Show()
	{
		if ( _rigidbody != null )
		{
			_rigidbody.Enabled = true;
		}

		if ( _collider != null )
		{
			_collider.Enabled = true;
		}

		if ( _model != null )
		{
			_model.Enabled = true;
		}
	}

	public void Hide()
	{
		if ( _rigidbody != null )
		{
			_rigidbody.Enabled = false;
		}

		if ( _collider != null )
		{
			_collider.Enabled = false;
		}

		if ( _model != null )
		{
			_model.Enabled = false;
		}
	}

	protected override void Interact( GameObject player )
	{
		var inventory = player.GetComponent<Inventory>();

		if ( inventory == null )
		{
			return;
		}
		
		inventory.TakeItem( GameObject );
		
		GameEvents.ItemCollected( GameObject );
	}
}
