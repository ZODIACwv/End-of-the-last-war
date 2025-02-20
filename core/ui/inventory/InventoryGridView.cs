using Godot;

public partial class InventoryGridView : GridContainer
{
    InventorySlotView[,] slots;
    Inventory inventory;
    public InventoryGridView Initialize(Inventory inventory)
    {
        this.inventory = inventory;
        slots = new InventorySlotView[inventory.width, inventory.height];
        PackedScene slotPrefab = GD.Load<PackedScene>("res://core/ui/inventory/InventorySlot.tscn");
        for (byte y = 0; y < inventory.height; y++)
            for (byte x = 0; x < inventory.width; x++)
            {
                InventorySlotView slot = slotPrefab.Instantiate<InventorySlotView>();
                slots[x, y] = slot;
                AddChild(slot);
            }

        return this;
    }

    public void HighlightCells(Vector2I? position, byte itemWidth, byte itemHeight)
    {
        bool canPlaceItem = true;

        UnhighlightAllCells();
        if (position is null) return;
        if (
            position.Value.Y + itemHeight > slots.GetLength(1) || 
            position.Value.Y < 0 ||
            position.Value.X + itemWidth > slots.GetLength(0) ||
            position.Value.X < 0
        ) return;

        for (int y = position.Value.Y; y < position.Value.Y + itemHeight; y++)
            for (int x = position.Value.X; x < position.Value.X + itemWidth; x++)
                if (!inventory.grid[x, y].CanPlaceItem())
                    canPlaceItem = false;

        for (int y = position.Value.Y; y < position.Value.Y + itemHeight; y++)
            for (int x = position.Value.X; x < position.Value.X + itemWidth; x++)
                if (canPlaceItem) slots[x, y].HighlightPositive();
                else slots[x, y].HighlightNegative();
    }

    public void UnhighlightAllCells()
    {
        for (byte y = 0; y < inventory.height; y++)
            for (byte x = 0; x < inventory.width; x++)
                slots[x, y].Unhighlight();
    }

#if DEBUG
    public void HighlightAllCells()
    {
        for (byte y = 0; y < inventory.height; y++)
            for (byte x = 0; x < inventory.width; x++)
                if (inventory.grid[x, y].CanPlaceItem()) slots[x, y].HighlightPositive();
                else slots[x, y].HighlightNegative();
    }
#endif

    public Vector2I? GetCellAtPosition(Vector2 globalPosition)
    {
        Vector2 localPosition = globalPosition - GetGlobalRect().Position;
        return new Vector2I(
            (int)(localPosition.X / TextureRuntimeSettings.slotSize),
            (int)(localPosition.Y / TextureRuntimeSettings.slotSize)
        );
    }
}
