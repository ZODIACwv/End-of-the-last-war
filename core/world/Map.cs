using System.Collections.Generic;
using Godot;

public partial class Map : TileMapLayer
{
	private List<Chunk> chunks;

	public override void _Ready()
	{
		chunks = new List<Chunk>(){
			new Chunk(this)
		};
		Texture2D a = GD.Load<Texture2D>("res://sprites/world/map/0_0.png");
		new Chunk(this).Load(a);
	}

	public Chunk GetChunk(Point2D position) {
		return chunks.Find(chunk => chunk.GetPosition().Equals(position));
	}
}
