using Godot;

public partial class Boostrap : Control
{
	AnimationPlayer animationPlayer;
	public override void _Ready()
	{
		Engine.MaxFps = (int)GetMeta("TargetFPS");

		animationPlayer = FindChild("BlinkingAnimator") as AnimationPlayer;
		animationPlayer.Play("Blinking/BlinkingIntro");
		animationPlayer.AnimationFinished += OnIntroFinished;
	}

	private void OnIntroFinished(StringName animName)
	{
		animationPlayer.AnimationFinished -= OnIntroFinished;
		animationPlayer.Play("Blinking/BlinkOn");
		animationPlayer.AnimationFinished += (StringName animName) =>
		{
			// Loading scene here:
			var menuScene = GD.Load<PackedScene>("res://Scenes/Menu/Menu.tscn");
			GetTree().ChangeSceneToPacked(menuScene);
		};
	}
}
