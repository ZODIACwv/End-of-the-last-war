using Godot;
public partial class InventoryItemView : ColorRect
{
    [Export] public TextureRect itemTexture;
    [Export] public ProgressBar endurance;
    [Export] public RichTextLabel name;
    internal InventoryItem item;
    Inventory inventory;
    InventoryView inventoryView;
    InventoryGridView inventoryGridView;
    TextureRect inventoryItemTexture;
    TextureRect draggingItem;

    public override void _Ready()
    {
        InventoryRoot inventoryRoot = GetNode<InventoryRoot>("../..");
        inventory = inventoryRoot.inventory;
        inventoryView = inventoryRoot.inventoryView;
        inventoryGridView = inventoryRoot.inventoryView.inventoryGridView;
        inventoryItemTexture = GetNode<TextureRect>("ItemTexture");
    }
    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left && draggingItem is null)
                StartDragMove();
            else if (!mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left && draggingItem is not null)
                EndDragMove();
        if (@event is InputEventMouseMotion motionEvent && draggingItem is not null)
                DragMove(motionEvent);
    }

    void StartDragMove()
    {
        draggingItem = new()
        {
            Name = "DraggingItem",
            Texture = inventoryItemTexture.Texture,
            Position = inventoryItemTexture.Position,
            Size = inventoryItemTexture.Size,
            Scale = new(0.25f, 0.25f)
        };

        inventoryItemTexture.Texture = null;
        inventory.Remove(item);
        AddChild(draggingItem);
    }
    void DragMove(InputEventMouseMotion motion)
    {
        draggingItem.GlobalPosition += motion.Relative * TextureRuntimeSettings.inventoryScale;
        inventoryGridView.HighlightCells(
            inventoryGridView.GetCellAtPosition(draggingItem.GlobalPosition), 
            item.width, 
            item.height
        );
    }
    void EndDragMove()
    {
        inventoryItemTexture.Texture = draggingItem.Texture;

        Vector2I? cellPos = inventoryGridView.GetCellAtPosition(draggingItem.GlobalPosition);
        GetNode("DraggingItem").QueueFree();
        draggingItem = null;

        if (cellPos is null) {
            inventory.Add(item);
            return;
        }
        if (inventory.CanPlaceItem(cellPos, item)) inventoryView.SnapToGrid(cellPos, this);
        else inventory.Add(item);
        inventoryGridView.UnhighlightAllCells();
    }
}