using Sandbox;

public sealed class Death : Component
{
    [Property] private bool destroyOnDeath { get; set; }
    [Property] private bool restartSceneOnDeath  { get; set; }
    [Property] private bool sendEnemyKilledEvent  { get; set; }
	private Stat stat;
	
	protected override void OnStart()
	{
		stat = GetComponent<Stat>();
        
        stat.OnDeath += Die;
	}
	
	protected override void OnDestroy()
    {
        if ( stat != null )
        {
            stat.OnDeath -= Die;
        }
    }
	
	private void Die()
	{
        if ( destroyOnDeath )
        {
            GameObject.Destroy();
        }

        if ( restartSceneOnDeath )
        {
	        Scene.LoadFromFile( Scene.Source.ResourcePath );
        }

        if ( sendEnemyKilledEvent )
        {
            GameEvents.EnemyKilled( GameObject );
        }
	}
}
