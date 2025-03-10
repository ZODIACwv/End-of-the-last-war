using Godot;

public partial class PlayerOnMap : Sprite2D
{
    [Export] MapGenerator mapGenerator;

    Vector2 targetPosition;
    bool isMoving = false;
    public double speed = 100.0f;

    TilesIDs currentBiome = TilesIDs.none;
    public delegate void OnBiomeChangedEvent(TilesIDs newBiome);
    public OnBiomeChangedEvent OnBiomeChanged;

    public override void _Ready() => OnBiomeChanged += ChangePlayerSpeedToBiome;

    public override void _Process(double delta)
    {
        if (!isMoving) return;

        TilesIDs newBiome = mapGenerator.GetTerrainTypeAt(GetPlayerTilePosition());
        if (currentBiome != newBiome)
        {
            OnBiomeChanged(newBiome);
            currentBiome = newBiome;
        }
        
        Vector2 velocity = (targetPosition - Position).Normalized() * (float)(speed * delta);

        if (Position.DistanceTo(targetPosition) < velocity.Length())
        {
            Position = targetPosition;
            isMoving = false;
        }
        else
            Position += velocity;
    }

    public void SetTarget(Vector2 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    Vector2I GetPlayerTilePosition()
    {
        Vector2 globalPosition = GlobalPosition;
        return new Vector2I((int)globalPosition.X / TextureRuntimeSettings.tileSize, (int)globalPosition.Y / TextureRuntimeSettings.tileSize);
    }

    void ChangePlayerSpeedToBiome(TilesIDs newBiome)
    {
        SM.Log(newBiome);
        speed = 100 * newBiome switch
        {
            TilesIDs.road => 1.1,
            TilesIDs.grass => 0.9,
            TilesIDs.wasteland => 1,
            TilesIDs.radioactiveWasteland => 1,
            TilesIDs.sea => 0.3,
            TilesIDs.none => 0.1,
            _ => 1
        };
    }

    static string GetBiomeName(int tileId) => tileId switch
    {
        (int)TilesIDs.grass => "grass",
        (int)TilesIDs.wasteland => "pustosh",
        (int)TilesIDs.sea => "sea",
        _ => "???",
    };
}
