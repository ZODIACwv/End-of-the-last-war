using Godot;

public partial class InventoryGridView : GridContainer
{
    byte width;
    byte height;

    public InventoryGridView Initialize(Inventory inventory)
    {
        width = inventory.width;
        height = inventory.height;
        PackedScene slot = GD.Load<PackedScene>("res://core/ui/inventory/InventorySlot.tscn");

        foreach (InventorySlot inventorySlot in inventory.grid)
            AddChild(slot.Instantiate());

        return this;
    }

    public void HighlightCells(InventoryItem item) => GD.Print("Highlighted");
}
