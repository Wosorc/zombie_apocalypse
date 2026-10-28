using Sandbox;
using System;
using System.IO;

public sealed class WeaponSystem : Component
{
	[Property] public WeaponType Type { get; private set; }
	[Property] public int Damage { get; private set; }
	
	[Property] public AmmoType AmmoType { get; private set; }
	[Property] public int CurrentAmmo { get; private set; }
	[Property] public int MaxAmmo { get; private set; }
	
	[Property] public float FireRate { get; private set; }
	[Property] public float Distance { get; private set; }
	
	private TimeSince timeSinceShot;
	
	public int Revision { get; private set; }
	
	public void PerformAction( GameObject player )
	{
		if ( timeSinceShot < FireRate )
			return;

		if (Type == WeaponType.Firearm )
		{
			if(CurrentAmmo <= 0 )
			{
				Log.Info( "Нет патронов" );
				return;
			}
			CurrentAmmo--;
			Shoot( player );
		}
		else if(Type == WeaponType.MeleeWeapon )
		{
			Attack( player );
		}

		timeSinceShot = 0;

		Revision++;
	}
	
	private SceneTraceResult SceneTrace( GameObject player, float distance )
	{
		var cameraComponent = player.Components.GetInChildren<CameraComponent>();

		var start = cameraComponent.WorldPosition;
		var end = start + cameraComponent.WorldRotation.Forward * distance;

		var tr = Scene.Trace
			.Ray( start, end )
			.IgnoreGameObject( player )
			.Run();
		return tr;
	}
	
	public void Shoot( GameObject player )
	{
		var tr = SceneTrace( player, Distance );

		if ( tr.Hit )
		{
			Log.Info( $"Попал в {tr.GameObject}" );

			Decal( tr );

			var enity = tr.GameObject.GetComponent<Stat>();

			if ( enity != null )
			{
				enity.TakeDamage( Damage );
			}
		}
	}
	
	public void Attack( GameObject player )
	{
		var tr = SceneTrace( player, Distance );

		if ( tr.Hit )
		{
			Log.Info( $"Попал в {tr.GameObject}" );
			
			Decal( tr );

			var enity = tr.GameObject.GetComponent<Stat>();

			if ( enity != null )
			{
				enity.TakeDamage( Damage );
			}
		}
	}
	
	public void Reload( GameObject player )
	{
		var dictionaryAmmo = player.GetComponent<DictionaryAmmo>();
		
		if ( !HasAmmo() || CurrentAmmo >= MaxAmmo )
		{
			return;
		}

		var amount = Math.Min( MaxAmmo - CurrentAmmo, dictionaryAmmo.GetAmmo( AmmoType ) );

		if ( amount <= 0 )
		{
			return;
		}

		CurrentAmmo += amount;
		dictionaryAmmo.RemoveAmmo( AmmoType, amount );

		Revision++;
	}
	
	public bool HasAmmo()
	{
		return Type == WeaponType.Firearm;
	}

	private void Decal( SceneTraceResult trace )
	{
		var decal = new GameObject();

		decal.Name = "decal";
		decal.WorldPosition = trace.EndPosition;
		decal.WorldRotation = Rotation.LookAt( -trace.Normal );

		var component = decal.AddComponent<Decal>();
		
		DecalDefinition decalDefinition = new DecalDefinition();
		decalDefinition.ColorTexture = Texture.Load( "icon/decals.png" ); 
		
		component.Decals.Add( decalDefinition );
		component.Size = 0.5f;
		component.LifeTime = 10f;
	}
}
