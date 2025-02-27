using Godot;

public partial class MapGenerator : Node2D
{
    [Export] Texture2D terrainTexture;
    [Export] Texture2D roadsTexture;
    [Export] TileMapLayer terrainLayer;
    [Export] TileMapLayer roadsLayer;

    Image terrainImage;
    Image roadsImage;

    public override void _Ready()
    {
        terrainImage = terrainTexture.GetImage();
        roadsImage = roadsTexture.GetImage();
        
        GenerateMap();
    }

    void GenerateMap()
    {

        for (int x = 0; x < terrainImage.GetWidth(); x++)
            for (int y = 0; y < terrainImage.GetHeight(); y++)
            {
                terrainLayer.SetCell(new Vector2I(x, y), GetTerrainType(terrainImage.GetPixel(x, y)), Vector2I.Zero);

                Color roadColor = roadsImage.GetPixel(x, y);
                if (IsRoad(roadColor))
                    roadsLayer.SetCell(new Vector2I(x, y), (int)TilesIDs.road, Vector2I.Zero);
            }
    }

    int GetTerrainType(Color color)
    {
        if (color.Equals(new Color(0f, 1f, 0f))) return (int)TilesIDs.grass;
        else if (color.Equals(new Color(1f, 1f, 0f))) return (int)TilesIDs.radioactiveWasteland;
        else if (color.Equals(new Color(0f, 0f, 1f))) return (int)TilesIDs.sea;
        return -1;
    }

    bool IsRoad(Color color) => color.R > 0.5f && color.G > 0.5f && color.B > 0.5f;
}

enum TilesIDs : int
{
    road = 0,
    grass = 1,
    wasteland = 2,
    radioactiveWasteland = 3,
    sea = 4
}