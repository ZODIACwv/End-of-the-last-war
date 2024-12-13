using Godot;

public partial class MainMenuButtons : Button
{
	[Export] MainMenuButtonType buttonType;
	public override void _Ready()
	{
		switch (buttonType)
		{
			case MainMenuButtonType.Continue:

				break;
			case MainMenuButtonType.NewGame:

				break;
			case MainMenuButtonType.Account:

				break;
			case MainMenuButtonType.News:

				break;
		}
	}
}

public enum MainMenuButtonType
{
	Continue,
	NewGame,
	Account,
	News
}
