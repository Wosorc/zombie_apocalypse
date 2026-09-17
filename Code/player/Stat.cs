using Sandbox;
using System;

public sealed class Stat : Component
{
	[Property] public float Health
	{
		get => _health;
		private set => _health = value.Clamp(0, MaxHealth);
	}
	[Property] public float Armor
	{
		get => _armor;
		private set => _armor = value.Clamp(0, MaxArmor); 
	}
	public bool IsDead => _isDead;
	[Property] public float MaxHealth { get; private set; } = 100f;
	[Property] public float MaxArmor { get; private set; } = 100f;
	public int Revision { get; private set; }
	public event Action OnDeath;
	private float _health;
	private float _armor;
	private bool _isDead;
	public void TakeDamage(float damage)
	{
		if ( _isDead || damage <= 0 )
		{
			 return;
		}

		Log.Info( $"Урон {damage} единиц нанесен по {GameObject.Name}" );

		if (Armor > 0)
		{
			float absorbed = MathF.Min(Armor, damage);

			Armor -= absorbed;
			damage -= absorbed;
		}
		
		if (damage > 0)
		{
			Health -= damage;
		}

		Revision++;

		if ( Health <= 0 )
		{
			Die();
		}
	}
	public void AddHealth( float amount )
	{
		if ( _isDead )
		{
			return;
		}

		Health += amount;
		Log.Info( $"Выдано {amount} единиц здоровья {GameObject}" );

		Revision++;
	}
	public void AddArmor( float amount )
	{
		if ( _isDead )
		{
			return;
		}

		Armor += amount;
		Log.Info( $"Выдано {amount} единиц брони {GameObject}" );

		Revision++;
	}
	private void Die()
	{
		_isDead = true;
		Log.Info( $"{GameObject.Name} умер" );

		OnDeath?.Invoke();

		Revision++;
	}
}
