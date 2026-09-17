using Sandbox;
using System.Collections.Generic;
using System.Runtime;

public sealed class DictionaryAmmo : Component
{
	[Property] private Dictionary<AmmoType, int> Ammo = new()
	{
		{ AmmoType.Pistol, 0 },
		{ AmmoType.Rifle, 0 },
		{ AmmoType.Shotgun, 0 }
	};
	public void AddAmmo(AmmoType type, int amount )
	{
		if ( Ammo.ContainsKey( type ) )
		{
			Ammo[type] += amount;

			Log.Info( $"Выдано {amount} единиц патронов типа {type}" );
		}
	}
	public void RemoveAmmo( AmmoType type, int amount )
	{
		if (Ammo.ContainsKey( type ) ) 
		{
			Ammo[type] -= amount;

			if ( amount != 0 )
			{
				Log.Info( $"Потрачено {amount} единиц патронов типа {type}" );
			}
			
		}
	}
	public int GetAmmo( AmmoType type )
	{
		if (Ammo.ContainsKey( type ) ) 
		{
			return Ammo[type];
		}
		
		return 0;
	}
}
