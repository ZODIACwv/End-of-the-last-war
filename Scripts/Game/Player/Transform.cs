using Godot;

public partial class Transform : Node2D
{
	[Export] float speed;
	[Export] float minDistanse;
	[Export] Node2D pointer;

	Vector2 direction;

	public override void _PhysicsProcess(double delta)
	{
		direction = pointer.GlobalPosition - GlobalPosition;
		if (GetGlobalTransform() != pointer.GetGlobalTransform() && direction.Length() > minDistanse)
		{
			direction = direction.Normalized();
			Position += new Vector2(direction.X * speed * (float)delta, direction.Y * speed * (float)delta);
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		// Проверяем, является ли событие кликом мыши
		if (@event is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.IsPressed())
			pointer.Position = GetGlobalMousePosition();
	}
}
