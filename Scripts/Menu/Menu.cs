using Godot;

public partial class Menu : Control
{
	public override void _Ready()
	{
		AnimationPlayer animPlayer = FindChild("BlinkOff") as AnimationPlayer;
		animPlayer.Play("Blinking/BlinkOff");
	}
}
