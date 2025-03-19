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
    {/*
        for (int x = 0; x < terrainImage.GetWidth(); x++)
            for (int y = 0; y < terrainImage.GetHeight(); y++)
            {
                //terrainLayer.SetCell(new Vector2I(x, y), (int)GetTerrainType(terrainImage.GetPixel(x, y)), Vector2I.Zero);

                if (IsRoad(roadsImage.GetPixel(x, y)))
                    roadsLayer.SetCell(new Vector2I(x, y), (int)RoadTilesIDs.road, new Vector2I(0, 0));
            }
        roadsLayer.UpdateInternals();*/

        Godot.Collections.Array<Vector2I> cells = [];

        for (int x = 0; x < terrainImage.GetWidth(); x++)
            for (int y = 0; y < terrainImage.GetHeight(); y++)
                if (IsRoad(roadsImage.GetPixel(x, y)))
                    cells.Add(new Vector2I(x, y));

        roadsLayer.SetCellsTerrainConnect(cells, 0, 0);
    }

    public TilesIDs GetTerrainTypeAt(Vector2I tilePosition) => GetTerrainType(terrainImage.GetPixel(tilePosition.X, tilePosition.Y), roadsImage.GetPixel(tilePosition.X, tilePosition.Y));

    static TilesIDs GetTerrainType(Color terrainPixel, Color roadPixel) => IsRoad(roadPixel) ? TilesIDs.road : GetTerrainType(terrainPixel);

    static TilesIDs GetTerrainType(Color color)
    {
        if (color.Equals(new Color(0f, 1f, 0f))) return TilesIDs.grass;
        else if (color.Equals(new Color(1f, 1f, 0f))) return TilesIDs.radioactiveWasteland;
        else if (color.Equals(new Color(0f, 0f, 1f))) return TilesIDs.sea;
        return TilesIDs.none;
    }

    static bool IsRoad(Color color) => color.R > 0.5f && color.G > 0.5f && color.B > 0.5f;
}

public enum TilesIDs : short
{
    road = 0,
    none = 1,
    grass = 2,
    wasteland = 3,
    radioactiveWasteland = 4,
    sea = 5
}