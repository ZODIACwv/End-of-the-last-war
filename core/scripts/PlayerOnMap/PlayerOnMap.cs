using Godot;

public partial class PlayerOnMap : Sprite2D
{
    Vector2 targetPosition;
    bool isMoving = false;
    [Export] public float speed = 100.0f; 

    public void SetTarget(Vector2 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    public override void _Process(double delta)
    {
        if (isMoving)
        {
            Vector2 direction = (targetPosition - Position).Normalized();
            Vector2 velocity = direction * speed * (float)delta;

            if (Position.DistanceTo(targetPosition) < velocity.Length())
            {
                Position = targetPosition;
                isMoving = false;
            }
            else
                Position += velocity;
        }
    }
}