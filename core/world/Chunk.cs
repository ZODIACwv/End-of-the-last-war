using Godot;

public class Chunk
{
	private Image chunkImage;
	private Point2D position;
	private TileMapLayer map;

	public Chunk(TileMapLayer map) {
		this.map = map;
	}

	public void Init() {
		chunkImage = new Image();
		position = Point2D.Zero;
	}

	public void Unload() {
		chunkImage = null;
	}

	public void Load(Texture2D texture) {
		chunkImage = texture.GetImage();

		int width = chunkImage.GetWidth();
		int height = chunkImage.GetHeight();

		byte[] pixelData = chunkImage.GetData();
		int bytesPerPixel = 3;

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				int index = (y * width + x) * bytesPerPixel;
				byte r = pixelData[index];
				byte g = pixelData[index + 1];
				byte b = pixelData[index + 2];
				Color color = new Color(r / 255f, g / 255f, b / 255f);
				string colorHtml = color.ToHtml(false);

				TileRegion region = Core.tileAtlas.Find(colorHtml);

				if (region is null) {
					GD.Print($"Pixel at ({x}, {y}): {colorHtml}");
					continue;
				}

				map.SetCell(new Vector2I(x, y), 0, new Vector2I(region.x, region.y));
				map.GetCellTileData(new Vector2I(x, y)).SetCustomData("ClassName", region.className);

			}
		}
	}

	public Point2D GetPosition() {
		return position;
	}
}
