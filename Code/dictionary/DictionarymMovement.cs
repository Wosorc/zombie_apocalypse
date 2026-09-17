using Sandbox;
using System.Collections.Generic;
public class MovementSettings
{
	public float Speed { get; set; }
	public float Height { get; set; }
}
public sealed class DictionarymMovement : Component
{
    private MovementType currentMovement = MovementType.Walking;
    public MovementSettings GetSettings()
    {
        return Settings[currentMovement];
    }
    public void SetMovementType( MovementType movementType )
    {
        currentMovement = movementType;
    }
    [Property] public Dictionary<MovementType, MovementSettings> Settings { get; private set; } = new()
    {
        {
            MovementType.Walking,
            new MovementSettings
            {
                Speed = 200f,
                Height = 55f
            }
        },
        {
            MovementType.Running,
            new MovementSettings
            {
                Speed = 350f,
                Height = 55f
            }
        },
        {
            MovementType.Crouching,
            new MovementSettings
            {
                Speed = 50f,
                Height = 30f
            }
        }
    };
    [Property] public float JumpHeight { get; private set; } = 250f;
}
