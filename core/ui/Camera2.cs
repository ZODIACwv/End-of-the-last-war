using Godot;

public partial class Camera2 : Camera2D
{
	[Export]
	public float speed = 1000f;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Vector2 inputDirection = new Vector2();

		if (Input.IsActionPressed("ui_up"))
		{
			inputDirection.Y -= 1;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			inputDirection.Y += 1;
		}
		if (Input.IsActionPressed("ui_left"))
		{
			inputDirection.X -= 1;
		}
		if (Input.IsActionPressed("ui_right"))
		{
			inputDirection.X += 1;
		}

		inputDirection = inputDirection.Normalized(); 

		Position += inputDirection * speed * (float)delta;
	}
}
