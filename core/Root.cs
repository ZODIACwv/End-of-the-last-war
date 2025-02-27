using Godot;

public partial class Root : Node
{
    public override void _Ready() {
        //GameDataLoader.Load();
        TextureRuntimeSettings.slotSize = 32;
        TextureRuntimeSettings.inventoryScale = 1f;
        TextureRuntimeSettings.slotNormal = GD.Load<Texture2D>("res://sprites/player/inventory/slot.png");
        TextureRuntimeSettings.slotHighlightedPositive = GD.Load<Texture2D>("res://sprites/player/inventory/slotHighlightedPositive.png");
        TextureRuntimeSettings.slotHighlightedNegative = GD.Load<Texture2D>("res://sprites/player/inventory/slotHighlightedNegative.png");
    }
}
