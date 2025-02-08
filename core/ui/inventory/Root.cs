using Godot;

public partial class Root : Node
{
    public override void _Ready() {
        GameDataLoader.Load();
        TextureRuntimeSettings.slotSize = 32;
        TextureRuntimeSettings.slotNormal = GD.Load<Texture2D>("res://core/ui/inventory/slot.png");
        TextureRuntimeSettings.slotHighlightedPositive = GD.Load<Texture2D>("res://core/ui/inventory/slotHighlightedPositive.png");
        TextureRuntimeSettings.slotHighlightedNegative = GD.Load<Texture2D>("res://core/ui/inventory/slotHighlightedNegative.png");
    }
}
