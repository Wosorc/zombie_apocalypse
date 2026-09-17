using Sandbox;
using System;

public sealed class Door : Interactable
{
	[Property] private Rotation Open { get; set; }
	[Property] private float SpeedMove { get; set; } = 10f;
	[Property] private bool Locked { get; set; }
	[Property, ShowIf( "Locked", true )] private GameObject UnLocker { get; set; }
	
	private bool _opened = false;
	private Rotation _close;
	private Rotation _targetRotation;

	protected override void OnStart()
	{
		_close = LocalRotation;
		_targetRotation = LocalRotation;
	}
	
	protected override void OnUpdate()
	{
		Move();
	}

	public void OpenClose()
	{
		_opened = !_opened;
		_targetRotation = _opened ? Open : _close;
	}

	private void Move()
	{
		LocalRotation = Rotation.Slerp(
			LocalRotation,
			_targetRotation,
			Time.Delta * SpeedMove
		);
	}

	protected override void Interact( GameObject player )
	{
		if ( Locked && !_opened )
		{
			var inventory = player.GetComponent<Inventory>();

			if ( inventory == null )
			{
				return;
			}
			
			if ( inventory.InventoryList.Contains( UnLocker ) )
			{
				OpenClose();	
			}
		}
		else
		{
			OpenClose();
		}
	}
}
