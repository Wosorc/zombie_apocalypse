using System;

public static class GameEvents
{
    public static event Action<GameObject> OnEnemyKilled;
    public static event Action<GameObject> OnItemCollected;
    public static event Action<GameObject> OnNpcTalked;
    public static event Action<GameObject> OnLocationReached;

    public static void EnemyKilled( GameObject target )
    {
        OnEnemyKilled?.Invoke( target );
    }

    public static void ItemCollected( GameObject target )
    {
        OnItemCollected?.Invoke( target );
    }

    public static void NpcTalked( GameObject target )
    {
        OnNpcTalked?.Invoke( target );
    }

    public static void LocationReached( GameObject target )
    {
        OnLocationReached?.Invoke( target );
    }
}