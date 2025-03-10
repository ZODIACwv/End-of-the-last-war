using Godot;

public partial class InventoryRoot : Control
{
    [Export] public byte borderRadius = 1;
    
    public InventoryView inventoryView;
    public Inventory inventory;

    public override void _Ready()
    {
        TextureRuntimeSettings.tileSize = 128;
        TextureRuntimeSettings.slotSize = 32;
        TextureRuntimeSettings.inventoryScale = 1f;
        TextureRuntimeSettings.slotNormal = GD.Load<Texture2D>("res://sprites/player/inventory/slot.png");
        TextureRuntimeSettings.slotHighlightedPositive = GD.Load<Texture2D>("res://sprites/player/inventory/slotHighlightedPositive.png");
        TextureRuntimeSettings.slotHighlightedNegative = GD.Load<Texture2D>("res://sprites/player/inventory/slotHighlightedNegative.png");

        MouseFilter = MouseFilterEnum.Ignore;

        // setting up the layout, inventory size should be evaluate with (n*TextureRuntimeSettings.slotSize + b*2), where n - slot quantity and b - border radius
        var grid = GetNode<GridContainer>("Grid");
        grid.Position = new Vector2(borderRadius, borderRadius);
        grid.Size = new Vector2(Size.X - borderRadius * 2, Size.Y - borderRadius * 2);
        grid.Columns = (int)(grid.Size.X / TextureRuntimeSettings.slotSize);

        inventory = new((byte)grid.Columns, (byte)(grid.Size.Y / TextureRuntimeSettings.slotSize));
        inventoryView = GetChild<InventoryView>(2).Initialize(this);

        
        // adding item
        InventoryItem item = new Item_AK74(2, 1, 50);
        InventoryItem item2 = new Item_Flask(2, 3, 75);
        inventory.Add(item);
        inventory.Add(item2);
        inventoryView.UpdateItemAdd(item);
        inventoryView.UpdateItemAdd(item2);
    }
}