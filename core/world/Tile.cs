using Godot;

public partial class Tile : Node2D
{
	public Vector2 position;

	public void Init(int x, int y) {
		position = new Vector2(x, y);
	}
}
