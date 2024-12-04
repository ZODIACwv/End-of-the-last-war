using Godot;

public partial class LoaderRotation : TextureRect
{
	float speed;
	public override void _Ready() => speed = (float)GetMeta("Speed");
	public override void _PhysicsProcess(double delta) => RotationDegrees += speed * (float)delta;
}
