using Godot;
using System.Collections.Generic;
using System.Text.Json;

public class TextureAtlas
{
	private AtlasTexture atlasTexture;
	private Dictionary<string, Rect2> regions;

	public TextureAtlas(string path)
	{
		atlasTexture = new AtlasTexture();
		atlasTexture.Atlas = GD.Load<Texture2D>(path);
		regions = new Dictionary<string, Rect2>();

		LoadJson(path.Replace(".png", ".json"));
	}

	private void LoadJson(string jsonPath)
	{
		string data = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read).GetAsText();
		JsonElement frames = JsonDocument.Parse(data).RootElement.GetProperty("frames");

		foreach (JsonProperty element in frames.EnumerateObject())
		{
			JsonElement frame = element.Value.GetProperty("frame");

			string name = element.Name;
			float x = frame.GetProperty("x").GetSingle();
			float y = frame.GetProperty("y").GetSingle();
			float width = frame.GetProperty("w").GetSingle();
			float height = frame.GetProperty("h").GetSingle();

			Rect2 region = new Rect2(x, y, width, height);
			regions[name] = region;

			GD.Print($"Region loaded: {name} - X: {x}, Y: {y}, Width: {width}, Height: {height}");
		}
	}

	public AtlasTexture Find(string name)
	{
		if (regions.TryGetValue(name, out Rect2 region))
		{
			AtlasTexture regionTexture = new AtlasTexture();
			regionTexture.Atlas = atlasTexture.Atlas;
			regionTexture.Region = region;
			return regionTexture;
		}

		GD.PrintErr("Region not found: " + name);
		return null;
	}
}
