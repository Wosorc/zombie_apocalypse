using Sandbox;

public sealed class BoxAmmo : Interactable
{
	[Property] private int AmmoAmount { get; set; }
	[Property] private AmmoType AmmoType { get; set; }
	protected override void Interact( GameObject player )
	{
		var dictionaryAmmo = player.GetComponent<DictionaryAmmo>();

		dictionaryAmmo.AddAmmo(AmmoType, AmmoAmount );
		GameEvents.ItemCollected( GameObject );
		GameObject.Destroy();
	}
}
