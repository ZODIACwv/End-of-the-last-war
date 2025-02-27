using Godot;

public partial class InventoryRoot : Control
{
    [Export] public byte borderRadius = 1;
    
    public InventoryView inventoryView;
    public Inventory inventory;

    public override void _Ready()
    {
        MouseFilter = MouseFilterEnum.Ignore;

        // setting up the layout, inventory size should be evaluate with (n*TextureRuntimeSettings.slotSize + b*2), where n - slot quantity and b - border radius
        var grid = GetNode<GridContainer>("Grid");
        grid.Position = new Vector2(borderRadius, borderRadius);
        grid.Size = new Vector2(Size.X - borderRadius * 2, Size.Y - borderRadius * 2);
        grid.Columns = (int)(grid.Size.X / TextureRuntimeSettings.slotSize);

        inventory = new((byte)grid.Columns, (byte)(grid.Size.Y / TextureRuntimeSettings.slotSize));
        inventoryView = GetChild<InventoryView>(2).Initialize(this);

        
        // adding item
        InventoryItem item = new AK74(2, 1, 50);
        InventoryItem item2 = new AK74(2, 3, 75);
        inventory.Add(item);
        inventory.Add(item2);
        inventoryView.UpdateItemAdd(item);
        inventoryView.UpdateItemAdd(item2);
    }
}