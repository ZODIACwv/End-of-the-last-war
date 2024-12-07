using Godot;

public partial class Menu : Control
{
	public override void _Ready()
	{
		AnimationPlayer animPlayer = FindChild("BlinkOff") as AnimationPlayer;
		animPlayer.Play("Blinking/BlinkOff");
		(FindChild("Button") as Button).ButtonDown += () => GD.Print("Log");
	}
	public void OnButtonPressed() => GD.Print("!!!");
}
