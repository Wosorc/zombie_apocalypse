using Sandbox;
using System;
using System.Numerics;

public sealed class ControllerPlayer : Component
{
	private Inventory inventory;
	private PlayerFlashlight spotLight;
	
	protected override void OnStart()
	{
		spotLight = GetComponent<PlayerFlashlight>();
		inventory = GetComponent<Inventory>();
	}
	
	protected override void OnUpdate()
	{
		HandleItemInput();
		HandleInventoryInput();
		HandleFlashlightInput();
	}
	
	private void HandleItemInput()
	{
		if ( Input.Down( "attack1" ) ) inventory.GetActiveWeapon()?.PerformAction( GameObject ); 
		if ( Input.Down( "reload" ) ) inventory.GetActiveWeapon()?.Reload( GameObject );
	}
	
	private void HandleInventoryInput()
	{
		if ( Input.MouseWheel.y != 0 ) inventory.SetActiveSlot( (inventory.ActiveSlot + Math.Sign( Input.MouseWheel.y ) + inventory.Slots) % inventory.Slots );
		if ( Input.Pressed( "Slot1" ) ) inventory.SetActiveSlot( 0 );
		if ( Input.Pressed( "Slot2" ) ) inventory.SetActiveSlot( 1 );
		if ( Input.Pressed( "Slot3" ) ) inventory.SetActiveSlot( 2 );
		if ( Input.Pressed( "drop" ) ) inventory.DropItem();
	}
	
	private void HandleFlashlightInput()
	{
		if ( Input.Pressed( "flashlight" ) ) spotLight.Toggle();
	}
}
