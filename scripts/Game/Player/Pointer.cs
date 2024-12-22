using Godot;

public partial class Pointer : Node2D
{
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.IsPressed())
			Position = GetGlobalMousePosition();
	}
}
