using Sandbox;

public sealed class Inventory : Component
{
	[Property] public int Slots = 3;
	[Property] public List<GameObject> InventoryList { get; private set; } = new List<GameObject>();
	public int ActiveSlot { get; private set; } = 0;
	public int Revision { get; private set; }

	public void TakeItem( GameObject item )
	{
		if ( InventoryList.Count >= Slots)
		{
			Log.Info( "Инвентарь заполнен" );
			return;
		}

		item.GetComponent<ItemSystem>().Hide();
		
		InventoryList.Add( item );
		Log.Info( $"Подобран предмет: {item.GetComponent<ItemSystem>().ItemName}" );

		Revision++;
	}
	
	public void DropItem()
	{
		if (ActiveSlot >= InventoryList.Count )
		{
			return;
		}
		
		var item = InventoryList[ActiveSlot];
		
		item.GetComponent<ItemSystem>().Show();
		
		var camera = GameObject.GetComponent<PlayerCamera>().camera;
		var rigibady = item.GetComponent<Rigidbody>();
		
		item.WorldRotation = camera.WorldRotation;
		item.WorldPosition = camera.WorldPosition + camera.WorldRotation.Forward * 20;

		if ( rigibady != null )
		{
			rigibady.Velocity = camera.WorldRotation.Forward * 100;
		}
		
		InventoryList.Remove( item );
		Log.Info( $"Выброшен предмет: {item.GetComponent<ItemSystem>().ItemName}" );

		Revision++;
	}
	
	public void SetActiveSlot( int slot )
	{
		if ( slot < 0 || slot >= Slots )
		{
			return;
		}

		if ( ActiveSlot == slot )
		{
			return;
		}

		ActiveSlot = slot;

		Revision++;
	}
	
	public ItemSystem GetActiveItem()
	{
		if ( ActiveSlot < 0 || ActiveSlot >= InventoryList.Count )
			return null;

		return InventoryList[ActiveSlot].GetComponent<ItemSystem>();
	}

	public WeaponSystem GetActiveWeapon()
	{
		if ( ActiveSlot < 0 || ActiveSlot >= InventoryList.Count )
			return null;

		return InventoryList[ActiveSlot].GetComponent<WeaponSystem>();
	}
	
	public ItemSystem GetItem( int number )
	{
		if ( number < 0 || number >= InventoryList.Count )
		{
			return null;
		}
		
		return InventoryList[number].GetComponent<ItemSystem>();
	}
	
	public WeaponSystem GetWeapon( int number )
	{
		if ( number < 0 || number >= InventoryList.Count )
			return null;

		return InventoryList[number].GetComponent<WeaponSystem>();
	}
}
