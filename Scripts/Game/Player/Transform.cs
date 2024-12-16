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
		if (@event is InputEventMouseButton)
		{
			InputEventMouseButton mouseEvent= @event as InputEventMouseButton;
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed == true)
			{
				pointer.SetGlobalPosition(mouseEvent.GetPosition());
			}
		}
	}
}
