using Godot;

public partial class Core : Node
{
	public static TileAtlas tileAtlas = new TileAtlas("res://sprites/world/tiles/tiles.json");

	public override void _Ready()
	{
		tileAtlas.Load();
		Texture2D a = GD.Load<Texture2D>("res://sprites/world/map/0_0.png");

	}
}
