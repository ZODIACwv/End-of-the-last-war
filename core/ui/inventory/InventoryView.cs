using Godot;

public partial class InventoryView : GridContainer
{
	Inventory inventory;
	public InventoryView Initialize(Inventory inventory)
	{
		this.inventory = inventory;
		var slot = GD.Load<PackedScene>("res://core/ui/inventory/InventorySlot.tscn");
		foreach (InventorySlot e in inventory.grid)
			AddChild(slot.Instantiate());
		return this;
	}
	public void UpdateView()
	{
		/*for (byte i = 0; i < inventory.grid.Length; i++) {
			InventorySlotView slot = inventory.grid[i % inventory.width, i / inventory.width];
			if (slot.itemSlot.item is not null && slot.itemSlot.isGeneralItemSlot)
			{
				
			}
		}*/
	}
	public void UpdateItem(InventoryItem item)
	{
		var addableItem = GD.Load<PackedScene>("res://core/ui/inventory/InventoryItem.tscn").Instantiate() as InventoryItemView;
        var addableItemTextureRect = addableItem.GetChildren()[0] as TextureRect;
        var addableItemEndurance = addableItem.GetChildren()[1] as ProgressBar;

        addableItem.Size = new(item.width * TextureRuntimeSettings.slotSize, item.height * TextureRuntimeSettings.slotSize);
		addableItem.Position = new();
		addableItemTextureRect.Texture = item.texture;
		addableItemTextureRect.Position = new();
        addableItemEndurance.Visible = !item.IsStackable;
        addableItemEndurance.Value = item.endurance ?? 0;

        GetChildren()[item.PositionX + item.PositionY * inventory.width].AddChild(addableItem);
	}
	public void AddItem(InventoryItem item) {
		inventory.Add(item);
		UpdateItem(item);
	}
	
	// TODO
	public void Increase(byte posY, byte posX, uint quantity) => inventory.Increase(posY, posX, quantity);
	public void Remove(byte posY, byte posX) => inventory.Remove(posY, posX);
	public void Decrease(byte posY, byte posX, uint quantity) => inventory.Decrease(posY, posX, quantity);
}

public class Inventory
{
	public InventorySlot[,] grid;
	public readonly byte width;
	public readonly byte height;

	public Inventory(byte width, byte height)
	{
		this.width = width;
		this.height = height;
		grid = new InventorySlot[width, height];

		for (byte y = 0; y < height; y++)
			for (byte x = 0; x < width; x++)
				grid[x, y] = new();
	}
	public bool CanPlaceItem(InventoryItem item)
	{
		if (item.PositionX + item.width > grid.GetLength(1) || item.PositionY + item.height > grid.GetLength(0)) return false;
		for (byte y = item.PositionY; y < item.PositionY + item.height; y++)
			for (byte x = item.PositionX; x < item.PositionX + item.width; x++)
				if (!grid[y, x].CanPlaceItem())
					return false;
		return true;
	}
	public void Add(InventoryItem item)
	{
		if (!CanPlaceItem(item)) return;
		grid[item.PositionX, item.PositionY].isGeneralItemSlot = true;
		for (byte y = 0; y < item.PositionY + item.height; y++)
			for (byte x = 0; x < item.PositionX + item.width; x++)
				grid[x, y].item = item;
	}

	// TODO
	public void Increase(byte posY, byte posX, uint quantity) { }
	public void Remove(byte posY, byte posX) { }
	public void Decrease(byte posY, byte posX, uint quantity) { }
}
