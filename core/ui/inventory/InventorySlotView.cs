using Godot;

public partial class InventorySlotView : TextureRect
{
    public override void _Ready() => Unhighlight();
    public void Unhighlight() => Texture = TextureRuntimeSettings.slotNormal;
    public void HighlightPositive() => Texture = TextureRuntimeSettings.slotHighlightedPositive;
    public void HighlightNegative() => Texture = TextureRuntimeSettings.slotHighlightedNegative;
}

public class InventorySlot
{
    public InventoryItem item = null;
    public bool isGeneralItemSlot = false;
    public bool CanPlaceItem() => item is null;
}

