using System.Collections.Generic;
using System.Text.Json;
using Godot;

public class TileAtlas {
	private string path;

	private Dictionary<string, TileRegion> colorTiles;
	public TileAtlas(string path) {
		this.path = path;
		colorTiles = new Dictionary<string, TileRegion>();
	}

	public void Load() {
		LoadJson(path);
	}

	private void LoadJson(string path) {
		string data = FileAccess.Open(path, FileAccess.ModeFlags.Read).GetAsText();

		JsonElement element = JsonDocument.Parse(data).RootElement;

		foreach(JsonElement e in element.EnumerateArray()) {

			string name = e.GetProperty("name").GetString();
			int x = e.GetProperty("x").GetInt32();
			int y = e.GetProperty("y").GetInt32();
			string color = e.GetProperty("color").GetString();
			string className = e.GetProperty("className").GetString();
			if(colorTiles.ContainsKey(color))
				continue;
			colorTiles.Add(color, new TileRegion(name, new Point2D(x, y), className));
		}
	}

	public TileRegion Find(string color) {
		colorTiles.TryGetValue(color, out TileRegion tileRegion);
		return tileRegion;
	}
}
